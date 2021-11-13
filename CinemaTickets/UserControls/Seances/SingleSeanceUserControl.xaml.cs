using CinemaTickets.Models;
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
        private Movie Movie { get { return _seance.IdMovieNavigation; } }
        private Hall Hall { get { return _seance.IdHallNavigation; } }

        public SingleSeanceUserControl(MoviesHall seance)
        {
            InitializeComponent();
            _seance = seance;
            LoadContent();
        }

        private void LoadContent()
        {
            MovieTitleTextBlock.Text = Movie.ToString();
            HallRoomTextBlock.Text = Hall.RoomNumber.ToString();
            HallSizeTextBlock.Text = Hall.Size.ToString();
            StartDateTextBlock.Text = _seance.StartTime.ToString();
            EndDateTextBlock.Text = _seance.EndTime.ToString();
        }
    }
}
