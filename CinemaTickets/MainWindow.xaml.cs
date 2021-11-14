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
using CinemaTickets.UserControls.Halls;
using CinemaTickets.UserControls.Seances;

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
        private readonly IHallRepository _hallRepository;
        private readonly ISeanceRepository _seanceRepository;
        private readonly ISeanceRegistrationRepository _seanceRegistrationRepository;

        public MainWindow(IAuthStore authStore, IAuthService authService, IClientRepository clientRepository, IPasswordCryption passwordCryption, IEmployeeRepository employeeRepository, IMovieRepository movieRepository, IHallRepository hallRepository, ISeanceRepository seanceRepository,
            ISeanceRegistrationRepository seanceRegistrationRepository)
        {
            _authStore = authStore;
            _authService = authService;
            _clientRepository = clientRepository;
            _passwordCryption = passwordCryption;
            _employeeRepository = employeeRepository;
            _movieRepository = movieRepository;
            _hallRepository = hallRepository;
            _seanceRepository = seanceRepository;
            _seanceRegistrationRepository = seanceRegistrationRepository;
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
                Content = "Home",
                MinWidth = 120
            };
            HomeButton.Click += OnHomeClick;
            NavigationStackPanel.Children.Add(HomeButton);
            if (_authStore.Type == AccountType.EMPLOYEE || _authStore.Type == AccountType.CLIENT)
            {
                var MoviesButton = new Button
                {
                    Content = "Movies",
                    MinWidth = 120
                };
                MoviesButton.Click += OnMoviesClick;

                var SeancesButton = new Button
                {
                    Content = "Seances",
                    MinWidth = 120
                };
                SeancesButton.Click += OnSeancesClick;


                NavigationStackPanel.Children.Add(MoviesButton);
                NavigationStackPanel.Children.Add(SeancesButton);
            }
            if (_authStore.Type == AccountType.EMPLOYEE)
            {
                var EmployeesButton = new Button
                {
                    Content = "Employees",
                    MinWidth = 120
                };
                EmployeesButton.Click += OnEmployeesClick;
                var HallsButton = new Button
                {
                    Content = "Halls",
                    MinWidth = 120
                };
                HallsButton.Click += OnHallsClick;

                NavigationStackPanel.Children.Add(EmployeesButton);
                NavigationStackPanel.Children.Add(HallsButton);
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
                    button.Click -= OnSeancesClick;
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

        private void ShowHalls()
        {
            if (_authStore.Login == null || _authStore.Type != AccountType.EMPLOYEE)
            {
                throw new NotLoggedException();
            }
            DisposeCurrentMainContentControl();
            MainContentControl.Content = new HallsUserControl(_hallRepository);
        }

        private void ShowSeances()
        {
            if (_authStore.Login == null)
            {
                throw new NotLoggedException();
            }
            DisposeCurrentMainContentControl();
            MainContentControl.Content = new SeancesUserControl(_movieRepository, _hallRepository, _seanceRepository, _authStore, _seanceRegistrationRepository);
        }

        private void ShowMovies()
        {
            if (_authStore.Login == null)
            {
                throw new NotLoggedException();
            }
            DisposeCurrentMainContentControl();
            MainContentControl.Content = new MoviesUserControl(_movieRepository, _authStore);
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

        private void OnSeancesClick(object sender, RoutedEventArgs e)
        {
            ShowSeances();
        }

        private void OnHallsClick(object sender, RoutedEventArgs e)
        {
            ShowHalls();
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
