using CinemaTickets.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaTickets.Authentication
{
    public interface IAuthService
    {
        long? Authenticate(AccountType type, string login, string password);
    }
}
