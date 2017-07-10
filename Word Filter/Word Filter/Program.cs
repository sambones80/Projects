using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Word_Filter
{
    class Program
    {
        static void Main(string[] args)
        {

            // Initialize the values
            string sentence = string.Empty;
            string[] words = { };
            string longWord = string.Empty;
            int minLimit = 0;
            string response = string.Empty;

            // Ask user to input a sentence
            Console.WriteLine("--- Word Filter ---");
            Console.WriteLine("");
            Console.WriteLine("Type a sentence:");
            sentence = Console.ReadLine();

            // Ask user for the minimum character number
            Console.WriteLine("");
            Console.WriteLine("This program will list the words longer than a certain number of letters.");
            Console.WriteLine("Enter the minimum number of letters:");

            bool result = Int32.TryParse(Console.ReadLine(), out minLimit);

            // Split sentence into individual words
            words = sentence.Split(' ');

            // Sort out the words that equal or exceed the minimum number of characters
            List<string> sortedList = words.OrderByDescending(w => w.Length).ToList();

            List<string> uniqueList = new List<string>();

            foreach (string word in sortedList)
            {
                if (word.Length >= minLimit)
                {
                    var item = uniqueList.Find(x => x == word);
                    if (item == null)
                    {
                        uniqueList.Add(word);
                    }
                }
            }

            // Print results to console
            Console.WriteLine("");
            Console.WriteLine("The words that are longer than {0} letters are ", minLimit);
            foreach (string word in uniqueList)
            {
                Console.WriteLine(word);
            }
            Console.Read();
        }
    }
}
