using CinemaTickets.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaTickets.Repositories
{
    public class HallRepository : IHallRepository
    {
        private readonly CinematicketsContext _context;

        public HallRepository(CinematicketsContext context)
        {
            _context = context;
        }
        public async Task<Hall> AddHallAsync(Hall hall)
        {
            _context.Halls.Add(hall);
            await _context.SaveChangesAsync();
            return hall;
        }

        public List<Hall> GetHalls()
        {
            return _context.Halls.ToList();
        }

        public async Task RemoveHallAsync(Hall hall)
        {
            _context.Halls.Remove(hall);
            await _context.SaveChangesAsync();
        }

        public Task<Hall> UpdateHallAsync(Hall hall)
        {
            _context.Halls.Update(hall);
            throw new NotImplementedException();
        }
    }
}
