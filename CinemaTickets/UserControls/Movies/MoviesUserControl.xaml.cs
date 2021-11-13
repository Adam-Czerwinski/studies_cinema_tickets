using CinemaTickets.Reactive;
using CinemaTickets.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
namespace CinemaTickets.UserControls.Movies
{
    /// <summary>
    /// Interaction logic for MoviesUserControl.xaml
    /// </summary>
    public partial class MoviesUserControl : UserControl, IDisposable
    {
        private readonly Subject<object?> _destroySubject = new();
        private readonly IMovieRepository _movieRepository;
        private bool disposedValue;

        public MoviesUserControl(IMovieRepository movieRepository)
        {
            InitializeComponent();
            _movieRepository = movieRepository;
            MovieReactiveUtils.DeleteMovieObservable.TakeUntil(_destroySubject).Subscribe(async id =>
            {
                await _movieRepository.RemoveMovieAsync(new()
                {
                    Id = id
                });
                MessageBox.Show("Deleted successfully");
                ShowMovies();
                AddButton.Visibility = Visibility.Visible;
            });
            MovieReactiveUtils.CancelEditMovieObservable.TakeUntil(_destroySubject).Subscribe(control =>
            {
                ShowMovies();
                AddButton.Visibility = Visibility.Visible;
            });

            MovieReactiveUtils.EditMovieObservable.TakeUntil(_destroySubject).Subscribe(control =>
            {
                foreach (SingleMovieUserControl movieControl in MoviesWrapPanel.Children)
                {
                    if (movieControl == control)
                    {
                        continue;
                    }

                    movieControl.HideButtons();
                }
                AddButton.Visibility = Visibility.Hidden;
            });

            MovieReactiveUtils.SaveMovieObservable.TakeUntil(_destroySubject).Subscribe(async movie =>
            {
                if (movie.Id is not null)
                {
                    await _movieRepository.UpdateMovieAsync(movie);
                    MessageBox.Show("Movie has been updated successfully");
                }
                else
                {
                    await _movieRepository.AddMovieAsync(movie);
                    MessageBox.Show("Movie has been added successfully");
                }
                ShowMovies();
                AddButton.Visibility = Visibility.Visible;
            });

            ShowMovies();
        }

        private void ShowMovies()
        {
            MoviesWrapPanel.Children.Clear();
            List<Models.Movie> movies = _movieRepository.GetMovies();
            var singleMovieUserControls = movies.Select(movie =>
                new SingleMovieUserControl(movie)
            ).ToList();
            singleMovieUserControls.ForEach(movieUserControl => MoviesWrapPanel.Children.Add(movieUserControl));

        }

        private void OnAddMovieClick(object sender, RoutedEventArgs e)
        {
            foreach (SingleMovieUserControl item in MoviesWrapPanel.Children)
            {
                item.HideButtons();
            }
            MoviesWrapPanel.Children.Add(new SingleMovieUserControl(null));
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
        // ~MoviesUserControl()
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
