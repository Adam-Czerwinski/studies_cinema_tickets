using System;
using System.Collections.Generic;

#nullable disable

namespace CinemaTickets.Models
{
    public partial class Movie
    {
        public Movie()
        {
            ClientsMoviesHalls = new HashSet<ClientsMoviesHall>();
            MoviesHalls = new HashSet<MoviesHall>();
        }

        public long? Id { get; set; }
        public string Title { get; set; }
        public DateTime Year { get; set; }
        public int Duration { get; set; }

        public virtual ICollection<ClientsMoviesHall> ClientsMoviesHalls { get; set; }
        public virtual ICollection<MoviesHall> MoviesHalls { get; set; }

        public bool IsValid()
        {
            return !string.IsNullOrWhiteSpace(Title)
                && Duration is > 0 &&
                Year != new DateTime();
        }

        public override string ToString()
        {
            return $"{Title} ({Year.Year})";
        }
    }
}
