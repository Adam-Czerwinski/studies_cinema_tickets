using CinemaTickets.Exceptions;
using CinemaTickets.Models;
using CinemaTickets.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaTickets.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly CinematicketsContext _context;

        public ClientRepository(CinematicketsContext context)
        {
            _context = context;
        }

        public async Task<Client> AddClientAsync(Client client)
        {
            if (!ValidatorUtils.IsValidLogin(client.Login))
            {
                throw new ValidationException("Nieprawidłowy login");
            }
            _context.Clients.Add(client);
            await _context.SaveChangesAsync();
            return client;
        }

        public bool ExistsByLoginIgnoreCase(string login)
        {
            return _context.Clients.Any(client => client.Login.ToLower() == login.ToLower());
        }

        public Client? GetByLogin(string login)
        {
            return _context.Clients.SingleOrDefault(client => login.Equals(client.Login));
        }
    }
}
