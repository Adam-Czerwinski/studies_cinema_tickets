using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaTickets.Authentication
{
    public class PasswordCryption : IPasswordCryption
    {
        private readonly int key = 12;

        public string DecryptPassword(string password)
        {
            return CaesarCipher.Decrypt(password.ToCharArray(), key);
        }

        public string EncryptPassword(string password)
        {
            return CaesarCipher.Encrypt(password.ToCharArray(), key);
        }

        public bool Matches(string raw, string encrypted)
        {
            return string.Equals(EncryptPassword(raw), encrypted);
        }
    }
}
