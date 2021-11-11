using CinemaTickets.Authentication;
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

namespace CinemaTickets.Pages.Login
{
    /// <summary>
    /// Interaction logic for MainLoginUserControl.xaml
    /// </summary>
    public partial class MainLoginUserControl : UserControl
    {
        enum LoginType
        {
            CLIENT,
            EMPLOYEE
        }

        private LoginType _currentLoginType;
        private readonly IClientRepository _clientRepository;
        private readonly IAuthStore _authStore;
        private readonly IAuthService _authService;
        private readonly IPasswordCryption _passwordCryption;
        private readonly IEmployeeRepository _employeeRepository;

        public MainLoginUserControl(IAuthStore authStore, IAuthService authService, IClientRepository clientRepository, IPasswordCryption passwordCryption,
            IEmployeeRepository employeeRepository)
        {
            _clientRepository = clientRepository;
            _authStore = authStore;
            _authService = authService;
            _passwordCryption = passwordCryption;
            _employeeRepository = employeeRepository;
            InitializeComponent();
            InitStartupPage();
        }

        private void InitStartupPage()
        {
            SetLoginPage(LoginType.CLIENT);
        }

        private void SetLoginPage(LoginType loginType)
        {
            _currentLoginType = loginType;
            if (LoginType.CLIENT == loginType)
            {
                LoginContentControl.Content = new ClientLoginUserControl(_authStore, _authService, _clientRepository, _passwordCryption);
                ClientButton.IsEnabled = false;
                EmployeeButton.IsEnabled = true;
            }
            else
            {
                LoginContentControl.Content = new EmployeeLoginUserControl(_authStore, _authService,_employeeRepository, _passwordCryption);
                EmployeeButton.IsEnabled = false;
                ClientButton.IsEnabled = true;
            }
        }

        private void OnClientClick(object sender, RoutedEventArgs e)
        {
            SetLoginPage(LoginType.CLIENT);
        }

        private void OnEmployeeClick(object sender, RoutedEventArgs e)
        {
            SetLoginPage(LoginType.EMPLOYEE);
        }
    }
}
