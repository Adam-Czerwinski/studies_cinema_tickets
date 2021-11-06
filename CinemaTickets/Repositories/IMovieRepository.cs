using CinemaTickets.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaTickets.Repositories
{
    public interface IMovieRepository
    {
        Task<Movie> AddMovieAsync(Movie movie);

        Task<Movie> UpdateMovieAsync(Movie movie);

        Task RemoveMovieAsync(Movie movie);

        List<Movie> GetMovies();
    }
}
