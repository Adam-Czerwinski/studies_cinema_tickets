
using CinemaTickets.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaTickets.Authentication
{
    public class AuthStore : IAuthStore
    {
        public string? Login { get; private set; }
        public AccountType? Type { get; private set; }

        public bool IsLogged()
        {
            return Login != null;
        }

        public void Logout()
        {
            Login = null;
            Type = null;
        }

        public void Store(AccountType type, string login)
        {
            Login = login;
            Type = type;
        }
    }
}
