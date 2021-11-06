using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaTickets
{
    public class CaesarCipher
    {
        private CaesarCipher()
        {

        }
        public static string Encrypt(char[] secretMessage, int key)
        {
            char[] alphabet = new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };

            int length = secretMessage.Length;
            //Console.WriteLine(length);
            char[] encryptedMessage = new char[length];
            for (int i = 0; i < secretMessage.Length; i++)
            {
                var letter = secretMessage[i];
                int index = Array.IndexOf(alphabet, letter);
                int newIndex = (key + index) % 26;
                char newLetter = alphabet[newIndex];
                encryptedMessage[i] = newLetter;
                //Console.WriteLine($"{letter}, {index}");
            }

            string enMessage = string.Join("", encryptedMessage);
            //Console.WriteLine(enMessage);
            return enMessage;
        }

        public static string Decrypt(char[] secretMessage, int key)
        {
            char[] alphabet = new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };

            int length = secretMessage.Length;
            //Console.WriteLine(length);
            char[] encryptedMessage = new char[length];
            for (int i = 0; i < secretMessage.Length; i++)
            {
                var letter = secretMessage[i];
                int index = Array.IndexOf(alphabet, letter);
                int newIndex = (index - key) % 26;
                char newLetter = alphabet[newIndex];
                encryptedMessage[i] = newLetter;
            }

            string enMessage = String.Join("", encryptedMessage);
            return enMessage;
        }

    }
}
