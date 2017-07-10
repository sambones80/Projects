using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Max_of_Five
{
    class Program
    {
        static void Main(string[] args)
        {
            // Initialize the values
            int num1 = 0;
            int num2 = 0;
            int num3 = 0;
            int num4 = 0;
            int num5 = 0;

            // Introduction
            Console.WriteLine("--- Max of Five ---");
            Console.WriteLine("");
            Console.WriteLine("This program will accept five numbers and determine which one is the greatest.");
            Console.WriteLine("");

            // Ask user to input each number and parse string to integer
            Console.WriteLine("Enter the first number:");
            bool result1 = Int32.TryParse(Console.ReadLine(), out num1);

            Console.WriteLine("Enter the second number:");
            bool result2 = Int32.TryParse(Console.ReadLine(), out num2);

            Console.WriteLine("Enter the third number:");
            bool result3 = Int32.TryParse(Console.ReadLine(), out num3);

            Console.WriteLine("Enter the fourth number:");
            bool result4 = Int32.TryParse(Console.ReadLine(), out num4);

            Console.WriteLine("Enter the fifth number:");
            bool result5 = Int32.TryParse(Console.ReadLine(), out num5);

            // Create a list for the numbers
            List<int> numbers = new List<int>();

            // Add each number to the list
            numbers.Add(num1);
            numbers.Add(num2);
            numbers.Add(num3);
            numbers.Add(num4);
            numbers.Add(num5);

            // Find the largest integer
            int maximum = numbers.Max();

            // Print the result
            Console.WriteLine("");
            Console.WriteLine("The largest number is {0}", maximum);

            Console.Read();
        }
    }
}
