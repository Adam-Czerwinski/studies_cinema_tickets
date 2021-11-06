using CinemaTickets.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaTickets.Repositories
{
    public class SeanceRegistrationRepository : ISeanceRegistrationRepository
    {
        private readonly CinematicketsContext _context;

        public SeanceRegistrationRepository(CinematicketsContext context)
        {
            _context = context;
        }
        public async Task<ClientsMoviesHall> AttendAsync(ClientsMoviesHall clientsMoviesHall)
        {
            _context.ClientsMoviesHalls.Add(clientsMoviesHall);
            await _context.SaveChangesAsync();
            return clientsMoviesHall;
        }
    }
}
