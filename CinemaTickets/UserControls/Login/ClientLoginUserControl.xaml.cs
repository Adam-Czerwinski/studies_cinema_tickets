using CinemaTickets.Authentication;
using CinemaTickets.Models;
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
    /// Interaction logic for ClientLoginUserControl.xaml
    /// </summary>
    public partial class ClientLoginUserControl : UserControl
    {
        private readonly IAuthStore _authStore;
        private readonly IAuthService _authService;
        private readonly IClientRepository _clientRepository;
        private readonly IPasswordCryption _passwordCryption;

        public ClientLoginUserControl(IAuthStore authStore, IAuthService authService, IClientRepository clientRepository, IPasswordCryption passwordCryption)
        {
            _authStore = authStore;
            _authService = authService;
            _clientRepository = clientRepository;
            _passwordCryption = passwordCryption;
            InitializeComponent();
            UpdateIsEnabledSignInButton();
            UpdateIsEnabledSignUpButton();
        }

        private void UpdateIsEnabledSignInButton()
        {
            SignInLoginButton.IsEnabled = ValidatorUtils.IsValidLogin(LoginTextBox.Text) && !string.IsNullOrEmpty(PasswordBox.Password);
        }

        private void UpdateIsEnabledSignUpButton()
        {
            SignUpLoginButton.IsEnabled = ValidatorUtils.IsValidLogin(LoginTextBox.Text) && ValidatorUtils.IsValidPassword(PasswordBox.Password);
        }

        private void OnLoginTextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateIsEnabledSignInButton();
            UpdateIsEnabledSignUpButton();
        }


        private void OnPasswordChanged(object sender, RoutedEventArgs e)
        {
            UpdateIsEnabledSignInButton();
            UpdateIsEnabledSignUpButton();
        }

        private void OnSignInClick(object sender, RoutedEventArgs e)
        {
            SignInLoginButton.IsEnabled = false;
            var login = LoginTextBox.Text;
            var password = PasswordBox.Password;
            bool authenticated = _authService.Authenticate(Models.AccountType.CLIENT, login, password);
            if (authenticated)
            {
                _authStore.Store(Models.AccountType.CLIENT, login);
                MessageBox.Show("Login succeed");
            }
            else
            {
                MessageBox.Show("Incorrect login or password");
            }
            SignInLoginButton.IsEnabled = true;
        }
        private async void OnSignUpClick(object sender, RoutedEventArgs e)
        {
            SignUpLoginButton.IsEnabled = false;
            var login = LoginTextBox.Text;
            var password = PasswordBox.Password;
            if (_clientRepository.ExistsByLoginIgnoreCase(login))
            {
                MessageBox.Show("Login already in use");
            }
            else
            {
                var client = new Client()
                {
                    Login = login,
                    Password = _passwordCryption.EncryptPassword(password)
                };
                client = await _clientRepository.AddClientAsync(client);
                MessageBox.Show("Account created. You can sign in.");
            }

            SignUpLoginButton.IsEnabled = true;
        }

    }
}
