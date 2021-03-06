using CinemaTickets.Models;
using CinemaTickets.Reactive;
using CinemaTickets.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Reactive.Linq;
using CinemaTickets.Authentication;
using CinemaTickets.Exceptions;

namespace CinemaTickets.UserControls.Seances
{
    /// <summary>
    /// Interaction logic for SeancesUserControl.xaml
    /// </summary>
    public partial class SeancesUserControl : UserControl, IDisposable
    {
        private readonly IMovieRepository _movieRepository;
        private readonly IHallRepository _hallRepository;
        private readonly ISeanceRepository _seanceRepository;
        private readonly IAuthStore _authStore;
        private readonly ISeanceRegistrationRepository? _seanceRegistrationRepository;
        private readonly Subject<object?> _destroySubject = new();
        private bool disposedValue;

        public SeancesUserControl(IMovieRepository movieRepository, IHallRepository hallRepository, ISeanceRepository seanceRepository,
            IAuthStore authStore,
            ISeanceRegistrationRepository? seanceRegistrationRepository
            )
        {
            InitializeComponent();
            _movieRepository = movieRepository;
            _hallRepository = hallRepository;
            _seanceRepository = seanceRepository;
            _authStore = authStore;
            _seanceRegistrationRepository = seanceRegistrationRepository;

            if (_authStore.Type == AccountType.EMPLOYEE)
            {
                AddButton.Visibility = Visibility.Visible;
                SeanceReactiveUtils.CancelEditSeanceObservable.TakeUntil(_destroySubject).Subscribe(control =>
                {
                    ShowSeances();
                    AddButton.Visibility = Visibility.Visible;
                });

                SeanceReactiveUtils.SaveSeanceObservable.TakeUntil(_destroySubject).Subscribe(async seance =>
                {
                    try
                    {
                        await _seanceRepository.AddSeanceAsync(seance);
                    }
                    catch (Microsoft.EntityFrameworkCore.DbUpdateException ex)
                    {
                        if (ex.InnerException is Microsoft.Data.SqlClient.SqlException && ex.InnerException.Message.Contains("duplicate key"))
                        {
                            _seanceRepository.Detach(seance);
                            MessageBox.Show("Seance already exists");
                            return;
                        }
                    }
                    MessageBox.Show("Seance has been added successfully");
                    ShowSeances();
                    AddButton.Visibility = Visibility.Visible;
                }
                );
            }
            else
            {
                AddButton.Visibility = Visibility.Collapsed;
                if (_seanceRegistrationRepository is null)
                {
                    throw new ArgumentNullException(nameof(seanceRegistrationRepository));
                }

                SeanceReactiveUtils.AttendSeanceObservable.TakeUntil(_destroySubject).Subscribe(async seance =>
                {
                    if (_authStore.Id is null)
                    {
                        throw new NotLoggedException();
                    }
                    ClientsMoviesHall clientsMoviesHall = new()
                    {
                        IdClient = (long)_authStore.Id,
                        IdHall = seance.IdHall,
                        IdMovie = seance.IdMovie,
                    };
                    await _seanceRegistrationRepository!.AttendAsync(clientsMoviesHall);
                    MessageBox.Show("Attended");
                    ShowSeances();
                });

                SeanceReactiveUtils.CancelAttendSeanceObservable.TakeUntil(_destroySubject).Subscribe(async seance =>
                {
                    if (_authStore.Id is null)
                    {
                        throw new NotLoggedException();
                    }
                    ClientsMoviesHall clientsMoviesHall = new()
                    {
                        IdClient = (long)_authStore.Id,
                        IdHall = seance.IdHall,
                        IdMovie = seance.IdMovie,
                    };
                    await _seanceRegistrationRepository!.RemoveAttendAsync(clientsMoviesHall);
                    MessageBox.Show("Attend canceled");
                    ShowSeances();
                });
            }
            ShowSeances();
        }

        private void ShowSeances()
        {
            SeancesWrapPanel.Children.Clear();
            List<MoviesHall> seances = _seanceRepository.GetSeances();

            if (_authStore.Type == AccountType.EMPLOYEE)
            {
                foreach (var seance in seances)
                {
                    SeancesWrapPanel.Children.Add(new SingleSeanceUserControl(seance));
                }
            }
            else
            {
                if (_authStore.Id is null)
                {
                    throw new NotLoggedException();
                }

                var clientSeances = _seanceRegistrationRepository!.GetSeances((long)_authStore.Id);
                foreach (var seance in seances)
                {
                    var isAttending = IsAttending(clientSeances, seance.IdMovie, seance.IdHall);
                    SeancesWrapPanel.Children.Add(new SingleSeanceUserControl(seance, isAttending));
                }
            }
        }

        private bool IsAttending(List<ClientsMoviesHall> clientSeances, long idMoie, long idHall)
        {
            return clientSeances.Any(clientSeance => clientSeance.IdMovie == idMoie && clientSeance.IdHall == idHall);
        }

        private void OnAddSeanceClick(object sender, RoutedEventArgs e)
        {
            var movies = _movieRepository.GetMovies();
            var halls = _hallRepository.GetHalls();
            SeancesWrapPanel.Children.Add(new NewSeanceUserControl(movies, halls));
            AddButton.Visibility = Visibility.Hidden;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                    _destroySubject.OnNext(null);
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~SeancesUserControl()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
