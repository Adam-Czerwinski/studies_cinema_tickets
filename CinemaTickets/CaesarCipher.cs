using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaTickets
{
    public class CaesarCipher
    {
        private readonly static char[] alphabet = new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z',
             'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', '1', '2', '3', '5', '6', '7', '8', '9', '0', '!','@','#','$','%','^','&','*','(',')'};

        private CaesarCipher()
        {

        }

        private static char Cipher(char ch, int key)
        {
            if (!char.IsLetter(ch))
            {

                return ch;
            }

            char d = char.IsUpper(ch) ? 'A' : 'a';
            return (char)((((ch + key) - d) % 26) + d);
        }
        public static string Encrypt(char[] secretMessage, int key)
        {
            string output = string.Empty;

            foreach (char ch in secretMessage) { output += Cipher(ch, key); }

            return output;
        }

        public static string Decrypt(char[] secretMessage, int key)
        {
            return Encrypt(secretMessage, 26 - key);
        }

    }
}
