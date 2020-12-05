using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2020
{
    public class MainRunner
    {
        static void Main(string[] args)
        {
            var dayToRun = new Day5();
            var answer = dayToRun.Run();

            Console.WriteLine($"Answer for {dayToRun.GetType().Name} is {answer}");
            Console.ReadLine();
        }
    }
}
