using CinemaTickets.Authentication;
using CinemaTickets.Models;
using CinemaTickets.Pages.Login;
using CinemaTickets.Reactive;
using CinemaTickets.Repositories;
using System.Windows;
using System;
using CinemaTickets.UserControls.Home;
using CinemaTickets.Exceptions;

namespace CinemaTickets
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IAuthStore _authStore;
        private readonly IAuthService _authService;
        private readonly IClientRepository _clientRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IPasswordCryption _passwordCryption;

        public MainWindow(IAuthStore authStore, IAuthService authService, IClientRepository clientRepository, IPasswordCryption passwordCryption, IEmployeeRepository employeeRepository)
        {
            _authStore = authStore;
            _authService = authService;
            _clientRepository = clientRepository;
            _passwordCryption = passwordCryption;
            _employeeRepository = employeeRepository;
            InitializeComponent();
            InitContentControl();
        }

        private void InitContentControl()
        {
            MainContentControl.Content = new MainLoginUserControl(_authStore, _authService, _clientRepository, _passwordCryption, _employeeRepository);
            LoginReactiveUtils.LoginObservable.Subscribe(DoOnLogin);
        }

        private void DoOnLogin(AccountType accountType)
        {
            if (_authStore.Login == null)
            {
                throw new NotLoggedException();
            }
            LogoutButton.Content = GetLogoutButtonMessage();
            LogoutButton.Visibility = Visibility.Visible;
            MainContentControl.Content = new HomeUserControl(_authStore.Login);
        }

        private string GetLogoutButtonMessage()
        {
            var type = _authStore.Type == AccountType.CLIENT ? "Client" : "Employee";

            return $"{type} {_authStore.Login}";
        }

        private void OnLogoutClick(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBoxResult = MessageBox.Show("Do you want to log out?", "Logout", MessageBoxButton.YesNo);
            switch (messageBoxResult)
            {
                case MessageBoxResult.Yes:
                    DoOnLogout();
                    break;
                default: break;
            }
        }

        private void DoOnLogout()
        {
            _authStore.Logout();
            LogoutButton.Content = "";
            LogoutButton.Visibility = Visibility.Hidden;
            InitContentControl();
        }
    }
}
