using CinemaTickets.Models;
using CinemaTickets.Reactive;
using CinemaTickets.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
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

namespace CinemaTickets.UserControls.Halls
{
    /// <summary>
    /// Interaction logic for HallsUserControl.xaml
    /// </summary>
    public partial class HallsUserControl : UserControl, IDisposable
    {
        private readonly Subject<object?> _destroySubject = new();
        private readonly IHallRepository _hallRepository;
        private bool disposedValue;

        public HallsUserControl(IHallRepository hallRepository)
        {
            InitializeComponent();
            _hallRepository = hallRepository;
            HallReactiveUtils.CancelEditHallObservable.TakeUntil(_destroySubject).Subscribe(control =>
            {
                ShowHalls();
                AddButton.Visibility = Visibility.Visible;
            });

            HallReactiveUtils.EditHallObservable.TakeUntil(_destroySubject).Subscribe(control =>
            {
                foreach (SingleHallUserControl hallControl in HallsWrapPanel.Children)
                {
                    if (hallControl == control)
                    {
                        continue;
                    }

                    hallControl.HideButtons();
                }
                AddButton.Visibility = Visibility.Hidden;
            });

            HallReactiveUtils.SaveHallObservable.TakeUntil(_destroySubject).Subscribe(async hall =>
            {
                if (hall.Id is not null)
                {
                    Hall hallDb = _hallRepository.GetHallById(hall.Id)!;
                    if (hallDb.RoomNumber != hall.RoomNumber)
                    {
                        if (_hallRepository.ExistsByRoomNumber(hall.RoomNumber))
                        {
                            MessageBox.Show("Hall already exists");
                            return;
                        }
                    }
                    await _hallRepository.UpdateHallAsync(hall);
                    MessageBox.Show("Hall has been updated successfully");
                }
                else
                {
                    if (_hallRepository.ExistsByRoomNumber(hall.RoomNumber))
                    {
                        MessageBox.Show("Hall already exists");
                        return;
                    }
                    await _hallRepository.AddHallAsync(hall);
                    MessageBox.Show("Hall has been added successfully");
                }
                ShowHalls();
                AddButton.Visibility = Visibility.Visible;
            });

            ShowHalls();

        }
        private void ShowHalls()
        {
            HallsWrapPanel.Children.Clear();
            List<Models.Hall> halls = _hallRepository.GetHalls();
            var singleHallUserControls = halls.Select(hall =>
                new SingleHallUserControl(hall)
            ).ToList();
            singleHallUserControls.ForEach(hallUserControl => HallsWrapPanel.Children.Add(hallUserControl));

        }

        private void OnAddHallClick(object sender, RoutedEventArgs e)
        {
            foreach (SingleHallUserControl item in HallsWrapPanel.Children)
            {
                item.HideButtons();
            }
            HallsWrapPanel.Children.Add(new SingleHallUserControl(null));
            AddButton.Visibility = Visibility.Hidden;
        }


        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _destroySubject.OnNext(null);
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~HallsUserControl()
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
