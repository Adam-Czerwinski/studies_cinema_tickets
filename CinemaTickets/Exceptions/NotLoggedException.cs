using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CinemaTickets.Exceptions
{
    [Serializable]
    public class NotLoggedException : Exception
    {

        public NotLoggedException()
        {
        }

        public NotLoggedException(string? message) : base(message)
        {
        }

        public NotLoggedException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected NotLoggedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
