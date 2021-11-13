using CinemaTickets.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
namespace CinemaTickets.Repositories
{
    public class SeanceRepository : ISeanceRepository
    {
        private readonly CinematicketsContext _context;

        public SeanceRepository(CinematicketsContext context)
        {
            _context = context;
        }
        public async Task<MoviesHall> AddSeanceAsync(MoviesHall moviesHall)
        {
            _context.MoviesHalls.Add(moviesHall);
            await _context.SaveChangesAsync();
            return moviesHall;
        }

        public List<MoviesHall> GetSeances()
        {
            return _context.MoviesHalls.AsNoTracking()
                .Include(seance => seance.IdHallNavigation)
                .AsNoTracking()
                .Include(seance => seance.IdMovieNavigation)
                .AsNoTracking()
                .ToList();
        }

        public async Task RemoveSeanceAsync(MoviesHall moviesHall)
        {
            _context.MoviesHalls.Remove(moviesHall);
            await _context.SaveChangesAsync();
        }

        public async Task<MoviesHall> UpdateSeanceAsync(MoviesHall moviesHall)
        {
            _context.MoviesHalls.Update(moviesHall);
            await _context.SaveChangesAsync();
            return moviesHall;
        }
    }
}
