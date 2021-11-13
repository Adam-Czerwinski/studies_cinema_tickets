using CinemaTickets.Authentication;
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

namespace CinemaTickets.UserControls.Employees
{
    /// <summary>
    /// Interaction logic for SingleEmployeeUserControl.xaml
    /// </summary>
    public partial class SingleEmployeeUserControl : UserControl
    {
        private bool _editing = false;
        private readonly bool _new;

        public Employee Employee { get; set; }
        public SingleEmployeeUserControl(Employee? employee)
        {
            InitializeComponent();
            _new = employee is null || employee.Id is null;
            if (employee is not null)
            {
                Employee = employee;
                LoadContent();
                SetEditControls(false);
            }
            else
            {
                Employee = new();
                SetEditControls(true);
            }

        }
        private void LoadContent()
        {
            EmployeeLoginTextBlock.Text = Employee.Login;
            EmployeeNameTextBlock.Text = Employee.Name;
            EmployeeLastNameTextBlock.Text = Employee.LastName;
            EmployeePasswordPasswordBox.Password = Employee.Password;
        }

        private void SetEditControls(bool edit)
        {
            _editing = edit;

            EmployeeLoginTextBlock.IsEnabled = edit && _new;
            EmployeeNameTextBlock.IsEnabled = edit;
            EmployeeLastNameTextBlock.IsEnabled = edit;
            PasswordStackPanel.Visibility = edit ? Visibility.Visible : Visibility.Collapsed;
            EmployeePasswordPasswordBox.IsEnabled = edit;

            SaveButton.Visibility = edit ? Visibility.Visible : Visibility.Collapsed;
            CancelButton.Visibility = edit ? Visibility.Visible : Visibility.Collapsed;
            EditButton.Visibility = !edit ? Visibility.Visible : Visibility.Collapsed;

            SaveButton.IsEnabled = _editing && IsValidEmployee();
        }

        private bool IsValidEmployee()
        {
            var tempEmployee = new Employee()
            {
                Login = EmployeeLoginTextBlock.Text.Trim(),
                Name = EmployeeNameTextBlock.Text.Trim(),
                LastName = EmployeeLastNameTextBlock.Text.Trim(),
                Password = EmployeePasswordPasswordBox.Password.Trim()
            };
            return tempEmployee.IsValid(true);
        }

        public void HideButtons()
        {
            SaveButton.Visibility = Visibility.Collapsed;
            CancelButton.Visibility = Visibility.Collapsed;
            EditButton.Visibility = Visibility.Collapsed;
        }


        private void OnCancelClick(object sender, RoutedEventArgs e)
        {
            EmployeeReactiveUtils.OnCancelEditEmployee(this);
        }

        private void OnEditClick(object sender, RoutedEventArgs e)
        {
            SetEditControls(true);
            EmployeeReactiveUtils.OnEditEmployee(this);
        }

        private void OnSaveClick(object sender, RoutedEventArgs e)
        {
            Employee.Login = EmployeeLoginTextBlock.Text.Trim();
            Employee.Name = EmployeeNameTextBlock.Text.Trim();
            Employee.LastName = EmployeeLastNameTextBlock.Text.Trim();
            Employee.Password = EmployeePasswordPasswordBox.Password.Trim();
            EmployeeReactiveUtils.OnSaveEmployee(Employee);
        }

        private void OnEmployeeLoginTextChanged(object sender, TextChangedEventArgs e)
        {
            SaveButton.IsEnabled = IsValidEmployee();
        }

        private void OnEmployeeNameTextChanged(object sender, TextChangedEventArgs e)
        {
            SaveButton.IsEnabled = IsValidEmployee();
        }

        private void OnEmployeeLastNameTextChanged(object sender, TextChangedEventArgs e)
        {
            SaveButton.IsEnabled = IsValidEmployee();
        }

        private void OnEmployeePasswordChanged(object sender, RoutedEventArgs e)
        {
            SaveButton.IsEnabled = IsValidEmployee();
        }
    }
}
