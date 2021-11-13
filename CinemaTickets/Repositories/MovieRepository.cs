using CinemaTickets.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
namespace CinemaTickets.Repositories
{
    internal class MovieRepository : IMovieRepository
    {
        private readonly CinematicketsContext _context;

        public MovieRepository(CinematicketsContext context)
        {
            _context = context;
        }
        public async Task<Movie> AddMovieAsync(Movie movie)
        {
            _context.Movies.Add(movie);
            await _context.SaveChangesAsync();
            _context.Entry(movie).State = EntityState.Detached;
            return movie;
        }

        public List<Movie> GetMovies()
        {
            return _context.Movies
                .AsNoTracking()
                .ToList();
        }

        public async Task RemoveMovieAsync(Movie movie)
        {
            _context.Movies.Remove(movie);
            await _context.SaveChangesAsync();
        }

        public async Task<Movie> UpdateMovieAsync(Movie movie)
        {
            _context.Movies.Update(movie);
            await _context.SaveChangesAsync();
            _context.Entry(movie).State = EntityState.Detached;
            return movie;
        }
    }
}
