using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Longest_Word
{
    class Program
    {
        static void Main(string[] args)
        {
            // Initialize the value
            string sentence = string.Empty;

            // Introduction
            Console.WriteLine("--- Longest and Shortest Word Finder ---");
            Console.WriteLine("");
            Console.WriteLine("This program will accept a sentence and determine which word contains the largest amount of letters and which word contains the smallest amount of letters.");
            Console.WriteLine("");

            // Ask user to input a sentence
            Console.WriteLine("Type a sentence to find the longest and shortest words:");
            sentence = Console.ReadLine();

            // Break sentence apart into an array of strings
            string[] wordArray = sentence.Split(' ');

            // Sort the array by word length
            var sorted = wordArray.OrderBy(w => w.Length);

            // Create variables for the last (longest) and the first (shortest) words
            var longest = sorted.LastOrDefault();
            var shortest = sorted.FirstOrDefault();

            // Print the results
            Console.WriteLine("");
            Console.WriteLine("\"{0}\" is the longest word.", longest);
            Console.WriteLine("\"{0}\" is the shortest word.", shortest);
            Console.Read();
        }
    }
}
