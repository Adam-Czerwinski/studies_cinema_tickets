using CinemaTickets.Authentication;
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

namespace CinemaTickets.UserControls.Employees
{
    /// <summary>
    /// Interaction logic for EmployeesUserControl.xaml
    /// </summary>
    public partial class EmployeesUserControl : UserControl, IDisposable
    {
        private readonly Subject<object?> _destroySubject = new();
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IPasswordCryption _passwordCryption;
        private bool disposedValue;

        public EmployeesUserControl(IEmployeeRepository employeeRepository, IPasswordCryption passwordCryption)
        {
            InitializeComponent();
            _employeeRepository = employeeRepository;
            _passwordCryption = passwordCryption;
            EmployeeReactiveUtils.CancelEditEmployeeObservable.TakeUntil(_destroySubject).Subscribe(control =>
            {
                ShowEmployees();
                AddButton.Visibility = Visibility.Visible;
            });

            EmployeeReactiveUtils.EditEmployeeObservable.TakeUntil(_destroySubject).Subscribe(control =>
            {
                foreach (SingleEmployeeUserControl employeeControl in EmployeesWrapPanel.Children)
                {
                    if (employeeControl == control)
                    {
                        continue;
                    }

                    employeeControl.HideButtons();
                }
                AddButton.Visibility = Visibility.Hidden;
            });

            EmployeeReactiveUtils.SaveEmployeeObservable.TakeUntil(_destroySubject).Subscribe(async employee =>
            {
                if (employee.Id is not null)
                {
                    var updateEmployee = new Employee()
                    {
                        Id = employee.Id,
                        Login = employee.Login,
                        Name = employee.Name,
                        LastName = employee.LastName,
                        Password = _passwordCryption.EncryptPassword(employee.Password),
                    };
                    await _employeeRepository.UpdateEmployeeAsync(updateEmployee);
                    MessageBox.Show("Employee has been updated successfully");
                }
                else
                {
                    if (_employeeRepository.ExistsByLoginIgnoreCase(employee.Login))
                    {
                        MessageBox.Show("Login already in use");
                        return;
                    }
                    var newEmployee = new Employee()
                    {
                        Login = employee.Login,
                        Name = employee.Name,
                        LastName = employee.LastName,
                        Password = _passwordCryption.EncryptPassword(employee.Password),
                    };
                    await _employeeRepository.AddEmployeeAsync(newEmployee);
                    MessageBox.Show("Employee has been added successfully");
                }
                ShowEmployees();
                AddButton.Visibility = Visibility.Visible;
            });

            ShowEmployees();
        }

        private void ShowEmployees()
        {
            EmployeesWrapPanel.Children.Clear();
            List<Models.Employee> employees = _employeeRepository.GetEmployees();
            foreach (var employee in employees)
            {
                employee.Password = _passwordCryption.DecryptPassword(employee.Password);
            }
            var singleEmployeeUserControls = employees.Select(employee =>
                new SingleEmployeeUserControl(employee)
            ).ToList();
            singleEmployeeUserControls.ForEach(employeeUserControl => EmployeesWrapPanel.Children.Add(employeeUserControl));
        }

        private void OnAddEmployeeClick(object sender, RoutedEventArgs e)
        {
            foreach (SingleEmployeeUserControl item in EmployeesWrapPanel.Children)
            {
                item.HideButtons();
            }
            EmployeesWrapPanel.Children.Add(new SingleEmployeeUserControl(null));
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
        // ~EmployeesUserControl()
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
