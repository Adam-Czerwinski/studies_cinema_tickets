using CinemaTickets.Models;
using CinemaTickets.Reactive;
using System;
using System.Collections.Generic;
using System.Linq;
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

namespace CinemaTickets.UserControls.Seances
{
    /// <summary>
    /// Interaction logic for SingleSeanceUserControl.xaml
    /// </summary>
    public partial class SingleSeanceUserControl : UserControl
    {
        private readonly MoviesHall _seance;
        private bool? _attending;
        private Movie Movie { get { return _seance.IdMovieNavigation; } }
        private Hall Hall { get { return _seance.IdHallNavigation; } }

        public SingleSeanceUserControl(MoviesHall seance, bool? attending = null)
        {
            InitializeComponent();
            _seance = seance;
            _attending = attending;
            LoadContent();
            ShowButtons();
        }

        private void ShowButtons()
        {
            if (_attending is null)
            {
                AttendButton.Visibility = Visibility.Collapsed;
                return;
            }

            AttendButton.Visibility = Visibility.Visible;
            AttendButton.Content = (bool)_attending ? "Cancel attend" : "Attend";
        }

        private void LoadContent()
        {
            MovieTitleTextBlock.Text = Movie.ToString();
            HallRoomTextBlock.Text = Hall.RoomNumber.ToString();
            HallSizeTextBlock.Text = Hall.Size.ToString();
            StartDateTextBlock.Text = _seance.StartTime.ToString();
            EndDateTextBlock.Text = _seance.EndTime.ToString();
        }

        private void AttendButtonClick(object sender, RoutedEventArgs e)
        {

            if (_attending is null)
            {
                return;
            }

            if ((bool)_attending)
            {
                _attending = false;
                SeanceReactiveUtils.OnCancelAttendSeance(_seance);

            }
            else
            {
                _attending = true;
                SeanceReactiveUtils.OnAttendSeance(_seance);
            }
            ShowButtons();
        }
    }
}
