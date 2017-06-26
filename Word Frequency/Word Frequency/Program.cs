using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Word_Frequency
{
    class Program
    {
        static void Main(string[] args)
        {
            // Initialize the values
            string sentence = string.Empty;
            string[] words = { };
            string longWord = string.Empty;

            // Ask user to input a sentence
            Console.WriteLine("--- Word Frequency Finder ---");
            Console.WriteLine("");
            Console.WriteLine("This program will accept a sentence and determine how many times each word appears in the sentence.");
            Console.WriteLine("");
            Console.WriteLine("Type a sentence to see the frequency of each word:");
            sentence = Console.ReadLine();

            // Split sentence into individual words
            words = sentence.Split(' ');

            // Sort the list
            List<string> sortedList = words.OrderBy(w => w.Count()).ToList();

            // Counting list and Garbage list
            List<string> freqList = new List<string>();
            List<string> usedList = new List<string>();

            foreach (string word in sortedList)
            {
                freqList = sortedList.FindAll(x => x == word);

                // Ignore previously counted word
                var item = usedList.Find(x => x == word);
                if (item == null)
                {
                    usedList.Add(word);

                    // Print results
                    Console.WriteLine("\n{0} appears {1} time(s)", word, freqList.Count);
                }
            }
            Console.Read();
        }
    }
}
