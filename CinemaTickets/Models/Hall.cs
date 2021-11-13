using System;
using System.Collections.Generic;

#nullable disable

namespace CinemaTickets.Models
{
    public partial class Hall
    {
        public Hall()
        {
            ClientsMoviesHalls = new HashSet<ClientsMoviesHall>();
            MoviesHalls = new HashSet<MoviesHall>();
        }

        public long? Id { get; set; }
        public int RoomNumber { get; set; }
        public int Size { get; set; }

        public virtual ICollection<ClientsMoviesHall> ClientsMoviesHalls { get; set; }
        public virtual ICollection<MoviesHall> MoviesHalls { get; set; }

        internal bool IsValid()
        {
            return RoomNumber > 0 && Size > 0;
        }

        public override string ToString()
        {
            return $"Room: {RoomNumber}, Size: {Size}";
        }
    }
}
