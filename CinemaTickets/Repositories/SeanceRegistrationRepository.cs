using CinemaTickets.Models;
using Microsoft.EntityFrameworkCore;
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
            _context.Entry(clientsMoviesHall).State = EntityState.Detached;
            return clientsMoviesHall;
        }

        public List<ClientsMoviesHall> GetSeances(long clientId)
        {
            return _context.ClientsMoviesHalls.AsNoTracking().Where(seances => seances.IdClient == clientId).ToList();
        }

        public async Task RemoveAttendAsync(ClientsMoviesHall clientsMoviesHall)
        {
            _context.ClientsMoviesHalls.Remove(clientsMoviesHall);
            await _context.SaveChangesAsync();
        }
    }
}
