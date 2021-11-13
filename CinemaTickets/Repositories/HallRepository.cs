using CinemaTickets.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
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
            _context.Entry(hall).State = EntityState.Detached;
            return hall;
        }

        public bool ExistsByRoomNumber(int number)
        {
            return _context.Halls.Any(hall => hall.RoomNumber == number);
        }

        public Hall? GetHallById(long? id)
        {
            return _context.Halls.AsNoTracking().SingleOrDefault(hall => hall.Id == id);
        }

        public List<Hall> GetHalls()
        {
            return _context.Halls.AsNoTracking().ToList();
        }

        public async Task RemoveHallAsync(Hall hall)
        {
            _context.Halls.Remove(hall);
            await _context.SaveChangesAsync();
        }

        public async Task<Hall> UpdateHallAsync(Hall hall)
        {
            _context.Halls.Update(hall);
            await _context.SaveChangesAsync();
            _context.Entry(hall).State = EntityState.Detached;
            return hall;
        }
    }
}
