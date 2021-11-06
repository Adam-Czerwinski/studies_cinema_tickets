using CinemaTickets.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaTickets.Repositories
{
    public interface ISeanceRegistrationRepository
    {
        Task<ClientsMoviesHall> AttendAsync(ClientsMoviesHall clientsMoviesHall);
    }
}
