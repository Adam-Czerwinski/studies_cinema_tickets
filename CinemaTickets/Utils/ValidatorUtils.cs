using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CinemaTickets.Utils
{
    public class ValidatorUtils
    {
        private ValidatorUtils()
        {

        }

        public static bool IsValidPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                return false;
            }

            if (password.Contains(' '))
            {
                return false;
            }

            if (password.Length < 7 || password.Length > 254)
            {
                return false;
            }

            return true;
        }

        public static bool IsValidLogin(string login)
        {
            if (string.IsNullOrWhiteSpace(login))
            {
                return false;
            }

            if (login.Contains(' '))
            {
                return false;
            }

            if (login.Length < 4 || login.Length > 18)
            {
                return false;
            }

            return true;
        }

        public static bool IsNumber(string text)
        {
            Regex regex = new("[^0-9]+");
            return regex.IsMatch(text);
        }
    }
}
