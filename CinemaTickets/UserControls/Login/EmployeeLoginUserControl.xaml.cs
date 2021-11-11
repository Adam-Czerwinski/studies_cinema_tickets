using CinemaTickets.Authentication;
using CinemaTickets.Models;
using CinemaTickets.Reactive;
using CinemaTickets.Repositories;
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

namespace CinemaTickets.Pages.Login
{
    /// <summary>
    /// Interaction logic for EmployeeLoginUserControl.xaml
    /// </summary>
    public partial class EmployeeLoginUserControl : UserControl
    {

        private readonly IAuthStore _authStore;
        private readonly IAuthService _authService;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IPasswordCryption _passwordCryption;

        public EmployeeLoginUserControl(IAuthStore authStore, IAuthService authService, IEmployeeRepository employeeRepository, IPasswordCryption passwordCryption)
        {
            _authStore = authStore;
            _authService = authService;
            _employeeRepository = employeeRepository;
            _passwordCryption = passwordCryption;
            InitializeComponent();
            UpdateIsEnabledSignInButton();
        }

        private void UpdateIsEnabledSignInButton()
        {
            SignInLoginButton.IsEnabled = ValidatorUtils.IsValidLogin(LoginTextBox.Text) && !string.IsNullOrEmpty(PasswordBox.Password);
        }


        private void OnLoginTextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateIsEnabledSignInButton();
        }


        private void OnPasswordChanged(object sender, RoutedEventArgs e)
        {
            UpdateIsEnabledSignInButton();
        }

        private void OnSignInClick(object sender, RoutedEventArgs e)
        {
            SignInLoginButton.IsEnabled = false;
            var login = LoginTextBox.Text;
            var password = PasswordBox.Password;
            bool authenticated = _authService.Authenticate(AccountType.EMPLOYEE, login, password);
            if (authenticated)
            {
                _authStore.Store(AccountType.EMPLOYEE, login);
                MessageBox.Show("Login succeed");
                LoginReactiveUtils.OnLogin(AccountType.CLIENT);
            }
            else
            {
                MessageBox.Show("Incorrect login or password");
            }
            SignInLoginButton.IsEnabled = true;
        }
    }
}
