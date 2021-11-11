using CinemaTickets.Models;
using CinemaTickets.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaTickets.Authentication
{
    public class AuthService : IAuthService
    {
        private readonly IPasswordCryption _passwordCryption;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IClientRepository _clientRepository;

        public AuthService(IPasswordCryption passwordCryption, IEmployeeRepository employeeRepository, IClientRepository clientRepository)
        {
            _passwordCryption = passwordCryption;
            _employeeRepository = employeeRepository;
            _clientRepository = clientRepository;
        }
        public bool Authenticate(AccountType type, string login, string password)
        {
            if (AccountType.CLIENT == type)
            {
                return AuthenticateClient(login, password);
            }
            else
            {
                return AuthenticateEmployee(login, password);
            }
        }

        private bool AuthenticateEmployee(string login, string password)
        {
            if (!_employeeRepository.ExistsByLoginIgnoreCase(login))
            {
                return false;
            }

            Employee? employee = _employeeRepository.GetByLogin(login);
            if (employee is null)
            {
                return false;
            }

            return _passwordCryption.Matches(password, employee.Password);
        }

        private bool AuthenticateClient(string login, string password)
        {
            if (!_clientRepository.ExistsByLoginIgnoreCase(login))
            {
                return false;
            }

            Client? client = _clientRepository.GetByLogin(login);
            if (client is null)
            {
                return false;
            }

            return _passwordCryption.Matches(password, client.Password);
        }
    }
}
