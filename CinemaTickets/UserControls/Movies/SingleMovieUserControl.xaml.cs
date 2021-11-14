using CinemaTickets.Models;
using CinemaTickets.Repositories;
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
using System.Text.RegularExpressions;
using CinemaTickets.Utils;
using CinemaTickets.Reactive;

namespace CinemaTickets.UserControls.Movies
{
    /// <summary>
    /// Interaction logic for SingleMovieUserControl.xaml
    /// </summary>
    public partial class SingleMovieUserControl : UserControl
    {
        private bool _editing = false;
        public Movie Movie { get; set; }
        public SingleMovieUserControl(Movie? movie, bool readonlyView)
        {
            InitializeComponent();
            if (movie is not null)
            {
                Movie = movie;
                LoadContent();
                if (!readonlyView)
                {
                    SetEditControls(false);
                }
            }
            else
            {
                Movie = new()
                {
                    Year = DateTime.Now
                };
                if (!readonlyView)
                {
                    SetEditControls(true);
                }
            }
            if (readonlyView)
            {
                SetEditControls(false);
                HideButtons();
            }
        }

        private void SetEditControls(bool edit)
        {
            _editing = edit;

            MovieTitleTextBlock.IsEnabled = edit;
            MovieYearDatePicker.IsEnabled = edit;
            MovieDurationTextBlock.IsEnabled = edit;

            SaveButton.Visibility = edit ? Visibility.Visible : Visibility.Collapsed;
            CancelButton.Visibility = edit ? Visibility.Visible : Visibility.Collapsed;
            EditButton.Visibility = !edit ? Visibility.Visible : Visibility.Collapsed;
            DeleteButton.Visibility = !edit ? Visibility.Visible : Visibility.Collapsed;

            SaveButton.IsEnabled = _editing && IsValidMovie();
        }

        private bool IsValidMovie()
        {
            var tempMovie = new Movie()
            {
                Title = MovieTitleTextBlock.Text.Trim(),
                Year = MovieYearDatePicker.SelectedDate == null ? new DateTime() : MovieYearDatePicker.SelectedDate.Value.Date,
                Duration = string.IsNullOrEmpty(MovieDurationTextBlock.Text) ? 0 : int.Parse(MovieDurationTextBlock.Text)
            };
            return tempMovie.IsValid();
        }

        private void LoadContent()
        {
            MovieTitleTextBlock.Text = Movie.Title;
            MovieYearDatePicker.SelectedDate = Movie.Year.Date;
            MovieDurationTextBlock.Text = Movie.Duration.ToString();
        }


        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            e.Handled = ValidatorUtils.IsNumber(e.Text);
        }

        private void OnDeleteClick(object sender, RoutedEventArgs e)
        {
            MovieReactiveUtils.OnDeleteMovie(Movie.Id!.Value);
        }

        private void OnCancelClick(object sender, RoutedEventArgs e)
        {
            MovieReactiveUtils.OnCancelEditMovie(this);
        }

        private void OnEditClick(object sender, RoutedEventArgs e)
        {
            SetEditControls(true);
            MovieReactiveUtils.OnEditMovie(this);
        }

        public void HideButtons()
        {
            SaveButton.Visibility = Visibility.Collapsed;
            CancelButton.Visibility = Visibility.Collapsed;
            EditButton.Visibility = Visibility.Collapsed;
            DeleteButton.Visibility = Visibility.Collapsed;
        }

        private void OnSaveClick(object sender, RoutedEventArgs e)
        {
            Movie.Title = MovieTitleTextBlock.Text.Trim();
            Movie.Year = MovieYearDatePicker.SelectedDate!.Value.Date;
            Movie.Duration = int.Parse(MovieDurationTextBlock.Text);
            MovieReactiveUtils.OnSaveMovie(Movie);
        }

        private void OnMovieTitleTextChanged(object sender, TextChangedEventArgs e)
        {
            SaveButton.IsEnabled = IsValidMovie();
        }

        private void OnMovieDurationTextChanged(object sender, TextChangedEventArgs e)
        {
            SaveButton.IsEnabled = IsValidMovie();
        }

        private void OnMovieYearSelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            SaveButton.IsEnabled = IsValidMovie();
        }
    }
}
