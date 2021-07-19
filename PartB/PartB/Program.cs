using System;
using System.Linq;

namespace PartB
{
    class Program
    {
        static void Main(string[] args)
        {
            decimal[] numbers = { 10.61m, 7.92m, 6.98m, 9.14m, 3.13m };
            numbers = numbers.Reverse().ToArray();

            foreach (var element in numbers)
                Console.Write(element + " ");

            Console.WriteLine();
        }
    }
}
