using CinemaTickets.Utils;
using System;
using System.Collections.Generic;

#nullable disable

namespace CinemaTickets.Models
{
    public partial class Employee
    {
        public long? Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }

        public bool IsValid(bool validatePassword = false)
        {
            if (validatePassword && !ValidatorUtils.IsValidPassword(Password))
            {
                return false;
            }

            return !string.IsNullOrWhiteSpace(Name)
                && !string.IsNullOrWhiteSpace(LastName)
                && ValidatorUtils.IsValidLogin(Login);
        }
    }
}
