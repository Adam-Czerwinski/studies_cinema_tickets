using CinemaTickets.Models;
using CinemaTickets.Reactive;
using CinemaTickets.Utils;
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

namespace CinemaTickets.UserControls.Halls
{
    /// <summary>
    /// Interaction logic for SingleHallUserControl.xaml
    /// </summary>
    public partial class SingleHallUserControl : UserControl
    {
        private bool _editing = false;
        public Hall Hall { get; set; }
        public SingleHallUserControl(Hall? hall)
        {
            InitializeComponent();
            if (hall is not null)
            {
                Hall = hall;
                LoadContent();
                SetEditControls(false);
            }
            else
            {
                Hall = new()
                {
                    Size = 20
                };
                SetEditControls(true);
            }
        }

        private void SetEditControls(bool edit)
        {
            _editing = edit;

            HallNumberTextBlock.IsEnabled = edit;
            HallSizeTextBlock.IsEnabled = edit;

            SaveButton.Visibility = edit ? Visibility.Visible : Visibility.Collapsed;
            CancelButton.Visibility = edit ? Visibility.Visible : Visibility.Collapsed;
            EditButton.Visibility = !edit ? Visibility.Visible : Visibility.Collapsed;

            SaveButton.IsEnabled = _editing && IsValidHall();
        }

        private bool IsValidHall()
        {
            var parsedSize = int.TryParse(HallSizeTextBlock.Text.Trim(), out int size);
            var parsedRoom = int.TryParse(HallNumberTextBlock.Text.Trim(), out int room);

            if (!parsedSize || !parsedRoom)
            {
                return false;
            }
            var tempHall = new Hall()
            {
                Size = size,
                RoomNumber = room
            };
            return tempHall.IsValid();
        }

        private void LoadContent()
        {
            HallSizeTextBlock.Text = Hall.Size.ToString();
            HallNumberTextBlock.Text = Hall.RoomNumber.ToString();
        }


        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            e.Handled = ValidatorUtils.IsNumber(e.Text);
        }


        private void OnCancelClick(object sender, RoutedEventArgs e)
        {
            HallReactiveUtils.OnCancelEditHall(this);
        }

        private void OnEditClick(object sender, RoutedEventArgs e)
        {
            SetEditControls(true);
            HallReactiveUtils.OnEditHall(this);
        }

        public void HideButtons()
        {
            SaveButton.Visibility = Visibility.Collapsed;
            CancelButton.Visibility = Visibility.Collapsed;
            EditButton.Visibility = Visibility.Collapsed;
        }

        private void OnSaveClick(object sender, RoutedEventArgs e)
        {
            Hall.RoomNumber = int.Parse(HallNumberTextBlock.Text);
            Hall.Size = int.Parse(HallSizeTextBlock.Text);
            HallReactiveUtils.OnSaveHall(Hall);
        }

        private void OnHallSizeTextChanged(object sender, TextChangedEventArgs e)
        {
            SaveButton.IsEnabled = IsValidHall();
        }

        private void OnHallNumberTextChanged(object sender, TextChangedEventArgs e)
        {
            SaveButton.IsEnabled = IsValidHall();
        }

    }
}
