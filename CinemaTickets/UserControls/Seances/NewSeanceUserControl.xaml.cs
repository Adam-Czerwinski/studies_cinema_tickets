using CinemaTickets.Exceptions;
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
    /// Interaction logic for NewSeanceUserControl.xaml
    /// </summary>
    public partial class NewSeanceUserControl : UserControl
    {
        private readonly List<Movie> _movies;
        private readonly List<Hall> _halls;

        public NewSeanceUserControl(List<Movie> movies, List<Hall> halls)
        {
            InitializeComponent();
            _movies = movies;
            _halls = halls;
            MoviesComboBox.ItemsSource = _movies;
            HallsComboBox.ItemsSource = _halls;
            UpdateSaveButtonEnabled();
        }

        private void OnCancelClick(object sender, RoutedEventArgs e)
        {
            SeanceReactiveUtils.OnCancelEditSeance(this);
        }

        private void OnSaveClick(object sender, RoutedEventArgs e)
        {
            Movie? movie = (MoviesComboBox.SelectedItem as Movie);
            Hall? hall = (HallsComboBox.SelectedItem as Hall);
            DateTime? startTime = SeanceDateDatePicker.Value;
            if (movie is null || hall is null || startTime is null || movie.Id is null || hall.Id is null)
            {
                throw new UnexpectedValidationException();
            }
            else
            {
                MoviesHall seance = new()
                {
                    StartTime = (DateTime)startTime,
                    EndTime = ((DateTime)startTime).AddMinutes(movie.Duration),
                    IdMovie = (long)movie.Id,
                    IdHall = (long)hall.Id,
                };
                SeanceReactiveUtils.OnSaveSeance(seance);
            }
        }

        private void OnSeanceDateSelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateSaveButtonEnabled();
        }

        private void UpdateSaveButtonEnabled()
        {
            if (MoviesComboBox.SelectedIndex == -1)
            {
                SaveButton.IsEnabled = false;
                return;
            }

            if (HallsComboBox.SelectedIndex == -1)
            {
                SaveButton.IsEnabled = false;
                return;
            }

            if (SeanceDateDatePicker.Value == null || SeanceDateDatePicker.Value <= DateTime.Now)
            {
                SaveButton.IsEnabled = false;
                return;
            }

            SaveButton.IsEnabled = true;
        }

        private void HallsComboBoxSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateSaveButtonEnabled();
        }

        private void MoviesComboBoxSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateSaveButtonEnabled();
        }

        private void SeanceDateDatePickerValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            UpdateSaveButtonEnabled();
        }
    }
}
