using CinemaTickets.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Reactive.Linq;
using CinemaTickets.Reactive;

namespace CinemaTickets.Repositories
{
    public class SeanceRepository : ISeanceRepository
    {
        private readonly CinematicketsContext _context;

        public SeanceRepository(CinematicketsContext context)
        {
            _context = context;
            InitClearScheduler();
        }

        private void InitClearScheduler()
        {
            Observable.Interval(TimeSpan.FromSeconds(20)).ExhaustMap(x =>
            {
                DeleteOldSeances();
                return Observable.Return(x);
            }).Subscribe();

        }

        private async void DeleteOldSeances()
        {
            var oldSeances = _context.MoviesHalls.AsNoTracking().ToList()
                .Where(seance => seance.EndTime < DateTime.Now)
                .ToList();
            if (oldSeances.Count == 0)
            {
                return;
            }
            List<ClientsMoviesHall> odClientSeances = new();
            foreach (var oldSeance in oldSeances)
            {
                List<ClientsMoviesHall> clientsMoviesHalls = _context.ClientsMoviesHalls.AsNoTracking
                    ().Where(clientSeance => clientSeance.IdMovie == oldSeance.IdMovie && clientSeance.IdHall == oldSeance.IdHall).ToList();

                odClientSeances.AddRange(clientsMoviesHalls);
            }
            _context.RemoveRange(oldSeances);
            _context.RemoveRange(odClientSeances);
            await _context.SaveChangesAsync();
        }

        public async Task<MoviesHall> AddSeanceAsync(MoviesHall moviesHall)
        {
            _context.MoviesHalls.Add(moviesHall);
            await _context.SaveChangesAsync();
            _context.Entry(moviesHall).State = EntityState.Detached;
            return moviesHall;
        }

        public void Detach(object entity)
        {
            _context.Entry(entity).State = EntityState.Detached;
        }

        public List<MoviesHall> GetSeances()
        {
            return _context.MoviesHalls.AsNoTracking()
                .Include(seance => seance.IdHallNavigation)
                .AsNoTracking()
                .Include(seance => seance.IdMovieNavigation)
                .AsNoTracking()
                .ToList().Where(seance => seance.EndTime >= DateTime.Now).ToList();
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
            _context.Entry(moviesHall).State = EntityState.Detached;
            return moviesHall;
        }
    }
}
