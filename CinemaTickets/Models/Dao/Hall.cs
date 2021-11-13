using System;
using System.Collections.Generic;

#nullable disable

namespace CinemaTickets.Models
{
    public partial class Hall
    {
        public long? Id { get; set; }
        public int RoomNumber { get; set; }
        public int Size { get; set; }

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
