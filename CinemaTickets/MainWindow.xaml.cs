using CinemaTickets.Authentication;
using CinemaTickets.Models;
using CinemaTickets.Pages.Login;
using CinemaTickets.Reactive;
using CinemaTickets.Repositories;
using System.Windows;
using System;
using CinemaTickets.UserControls.Home;
using CinemaTickets.Exceptions;
using System.Windows.Controls;
using CinemaTickets.UserControls.Movies;
using System.Reactive.Linq;
using CinemaTickets.UserControls.Employees;

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
        private readonly IMovieRepository _movieRepository;

        public MainWindow(IAuthStore authStore, IAuthService authService, IClientRepository clientRepository, IPasswordCryption passwordCryption, IEmployeeRepository employeeRepository, IMovieRepository movieRepository)
        {
            _authStore = authStore;
            _authService = authService;
            _clientRepository = clientRepository;
            _passwordCryption = passwordCryption;
            _employeeRepository = employeeRepository;
            _movieRepository = movieRepository;
            InitializeComponent();
            InitContentControl();
        }

        private void InitContentControl()
        {
            DisposeCurrentMainContentControl();
            MainContentControl.Content = new MainLoginUserControl(_authStore, _authService, _clientRepository, _passwordCryption, _employeeRepository);
            LoginReactiveUtils.LoginObservable
                .Take(1)
                .Subscribe(_ =>
            {
                ShowHome();
                RebuildNavigation();
            });
        }

        private void DisposeCurrentMainContentControl()
        {
            if (MainContentControl.Content is null)
            {
                return;
            }

            if (MainContentControl.Content is IDisposable disposableContent)
            {
                disposableContent.Dispose();
            }
        }

        private void RebuildNavigation()
        {
            ClearNavigation();
            var isLogged = _authStore.Login != null;
            if (isLogged)
            {
                BuildNavigation();
            }
        }

        private void BuildNavigation()
        {
            var hasType = _authStore.Type != null;
            if (!hasType)
            {
                throw new NotLoggedException();
            }
            var type = _authStore.Type == AccountType.CLIENT ? "Client" : "Employee";

            LogoutButton.Content = $"{type} {_authStore.Login}";
            LogoutButton.Visibility = Visibility.Visible;

            var HomeButton = new Button
            {
                Content = "Home"
            };
            HomeButton.Click += OnHomeClick;
            NavigationStackPanel.Children.Add(HomeButton);
            if (_authStore.Type == AccountType.EMPLOYEE)
            {
                var MoviesButton = new Button
                {
                    Content = "Movies"
                };
                MoviesButton.Click += OnMoviesClick;
                var EmployeesButton = new Button
                {
                    Content = "Employees"
                };
                EmployeesButton.Click += OnEmployeesClick;
                var HallsButton = new Button
                {
                    Content = "Halls"
                };
                HallsButton.Click += OnHallsClick;
                NavigationStackPanel.Children.Add(MoviesButton);
                NavigationStackPanel.Children.Add(EmployeesButton);
                NavigationStackPanel.Children.Add(HallsButton);
            }
            else if (_authStore.Type == AccountType.CLIENT)
            {

            }
        }

        private void ClearNavigation()
        {
            LogoutButton.Content = "";
            LogoutButton.Visibility = Visibility.Hidden;
            foreach (var child in NavigationStackPanel.Children)
            {
                if (child is Button button)
                {
                    button.Click -= OnHomeClick;
                    button.Click -= OnMoviesClick;
                    button.Click -= OnEmployeesClick;
                    button.Click -= OnHallsClick;
                }
            }
            NavigationStackPanel.Children.Clear();
        }

        private void ShowEmployees()
        {
            if (_authStore.Login == null || _authStore.Type != AccountType.EMPLOYEE)
            {
                throw new NotLoggedException();
            }
            DisposeCurrentMainContentControl();
            MainContentControl.Content = new EmployeesUserControl(_employeeRepository, _passwordCryption);
        }

        private void ShowMovies()
        {
            if (_authStore.Login == null)
            {
                throw new NotLoggedException();
            }
            DisposeCurrentMainContentControl();
            MainContentControl.Content = new MoviesUserControl(_movieRepository);
        }

        private void ShowHome()
        {
            if (_authStore.Login == null)
            {
                throw new NotLoggedException();
            }
            DisposeCurrentMainContentControl();
            MainContentControl.Content = new HomeUserControl(_authStore.Login);
        }

        private void OnHallsClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void OnEmployeesClick(object sender, RoutedEventArgs e)
        {
            ShowEmployees();
        }

        private void OnMoviesClick(object sender, RoutedEventArgs e)
        {
            ShowMovies();
        }

        private void OnHomeClick(object sender, RoutedEventArgs e)
        {
            ShowHome();
        }

        private void OnLogoutClick(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBoxResult = MessageBox.Show("Do you want to log out?", "Logout", MessageBoxButton.YesNo);
            switch (messageBoxResult)
            {
                case MessageBoxResult.Yes:
                    _authStore.Logout();
                    RebuildNavigation();
                    InitContentControl();
                    break;
                default: break;
            }
        }
    }
}
