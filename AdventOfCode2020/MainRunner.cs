using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2020
{
    public class MainRunner
    {
        static void Main(string[] args)
        {
            var answer = new Day4().Run();

            Console.WriteLine($"Answer is {answer}");
            Console.ReadLine();
        }
    }
}
