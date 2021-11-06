using System;
using System.Collections.Generic;

#nullable disable

namespace CinemaTickets.Models
{
    public partial class ClientsMoviesHall
    {
        public long IdClient { get; set; }
        public long IdMovie { get; set; }
        public long IdHall { get; set; }

        public virtual Client IdClientNavigation { get; set; }
    }
}
