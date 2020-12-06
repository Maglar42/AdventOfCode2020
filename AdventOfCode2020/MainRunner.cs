using System;

namespace AdventOfCode2020
{
    public class MainRunner
    {
        static void Main()
        {
            var dayToRun = new Day6();
            var answer = dayToRun.Run();

            Console.WriteLine($"Answer for {dayToRun.GetType().Name} is {answer}");
            Console.ReadLine();
        }
    }
}
