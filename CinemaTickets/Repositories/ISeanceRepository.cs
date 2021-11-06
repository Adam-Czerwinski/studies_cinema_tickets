using CinemaTickets.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaTickets.Repositories
{
    public interface ISeanceRepository
    {
        Task<MoviesHall> AddSeanceAsync(MoviesHall moviesHall);

        Task<MoviesHall> UpdateSeanceAsync(MoviesHall moviesHall);

        Task RemoveSeanceAsync(MoviesHall moviesHall);

        List<MoviesHall> GetSeances();
    }
}
