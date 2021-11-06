using CinemaTickets.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaTickets.Repositories.Auth
{
    public interface IAuthStore
    {
        string? Login { get; set; }
        AccountType? Type { get; set; }
        void Store(AccountType type, string login);

        bool IsLogged();

        void Logout();
    }
}
