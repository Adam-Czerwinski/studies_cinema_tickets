using CinemaTickets.Authentication;
using CinemaTickets.Pages.Login;
using CinemaTickets.Repositories;
using System.Windows;

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
            InitFrame();
        }

        private void InitFrame()
        {
            MainContentControl.Content = new MainLoginUserControl(_authStore, _authService, _clientRepository, _passwordCryption, _employeeRepository);
        }
    }
}
