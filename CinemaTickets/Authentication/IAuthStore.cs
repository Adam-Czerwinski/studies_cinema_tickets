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
        long? Id { get; }
        void Store(AccountType type, string login, long id);

        bool IsLogged();

        void Logout();
    }
}
