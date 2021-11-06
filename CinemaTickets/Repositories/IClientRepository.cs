using CinemaTickets.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaTickets.Repositories
{
    public interface IClientRepository
    {
        Task<Client> AddClientAsync(Client client);

        bool ExistsByLogin(string login);
    }
}
