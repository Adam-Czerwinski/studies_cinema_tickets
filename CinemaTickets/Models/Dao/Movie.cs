using System;
using System.Collections.Generic;

#nullable disable

namespace CinemaTickets.Models
{
    public partial class Movie
    {
        public long? Id { get; set; }
        public string Title { get; set; }
        public DateTime Year { get; set; }
        public int Duration { get; set; }

        public bool IsValid()
        {
            return !string.IsNullOrWhiteSpace(Title)
                && Duration is > 0 &&
                Year != new DateTime();
        }
    }
}
