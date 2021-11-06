using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaTickets.Repositories
{
    public interface IPasswordCryption
    {
        string EncryptPassword(string password);

        string DecryptPassword(string password);

        bool Matches(string raw, string encrypted);
    }
}
