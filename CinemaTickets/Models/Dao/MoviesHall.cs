using System;
using System.Collections.Generic;

#nullable disable

namespace CinemaTickets.Models
{
    public partial class MoviesHall
    {
        public long IdMovie { get; set; }
        public long IdHall { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public virtual Hall IdHallNavigation { get; set; }
        public virtual Movie IdMovieNavigation { get; set; }
    }
}
