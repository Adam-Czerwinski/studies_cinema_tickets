using CinemaTickets.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaTickets.Authentication
{
    public interface IAuthStore
    {
        string? Login { get; }
        AccountType? Type { get; }
        void Store(AccountType type, string login);

        bool IsLogged();

        void Logout();
    }
}
