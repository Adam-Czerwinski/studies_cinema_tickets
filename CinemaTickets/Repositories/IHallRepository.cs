using CinemaTickets.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaTickets.Repositories
{
    internal interface IHallRepository
    {
        Task<Hall> AddHallAsync(Hall hall);

        Task<Hall> UpdateHallAsync(Hall hall);

        Task RemoveHallAsync(Hall hall);

        List<Hall> GetHalls();
    }
}
