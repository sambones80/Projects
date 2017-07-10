using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Palindrome
{
    class Program
    {
        static void Main(string[] args)
        {
            // Initialize the value
            string palWord = string.Empty;

            // Introduction
            Console.WriteLine("--- Palindrome Checker ---");
            Console.WriteLine("");
            Console.WriteLine("This program will accept a word and determine if it is a palindrome.");
            Console.WriteLine("");

            // Ask user to input a word
            Console.WriteLine("Enter a word to see if it's a palindrome:");
            palWord = Console.ReadLine();

            // Break string apart to array of characters
            char[] palArray = palWord.ToCharArray();

            // Reverse the order of characters in the array
            Array.Reverse(palArray);

            // Create new string from the reversed array
            string revWord = new string(palArray);

            // Compare user's original word to the reversed word and print results
            if (palWord == revWord)
            {
                Console.WriteLine("");
                Console.WriteLine("{0} is a palindrome!", palWord);
            }
            else
            {
                Console.WriteLine("");
                Console.WriteLine("{0} is not a palindrome.", palWord);
            }

            Console.Read();
        }
    }
}
