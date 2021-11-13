using System;
using System.Collections.Generic;

#nullable disable

namespace CinemaTickets.Models
{
    public partial class Client
    {
        public Client()
        {
            ClientsMoviesHalls = new HashSet<ClientsMoviesHall>();
        }

        public long? Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }

        public virtual ICollection<ClientsMoviesHall> ClientsMoviesHalls { get; set; }
    }
}
