using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode2020
{
    // https://adventofcode.com/2020/day/9
    public class Day09
    {
        public string Run()
        {
            var fullInput = InputHelper.ReadOutEachLine("Day9Input");
            var nums = new List<long>();
            foreach(var input in fullInput)
            {
                nums.Add(long.Parse(input));
            }
            
            // Test input vs real input
            int howFarToLookBack = nums.Count < 25 ? 5 : 25;
            int indexToCheck = howFarToLookBack;

            bool numberChecksOut;
            do
            {
                var intToVerify = nums[indexToCheck];

                numberChecksOut = false;
                // outerloop does not need to look at max and inner loop does not need to look at first
                for (int outerLookBack = 1; outerLookBack < howFarToLookBack; outerLookBack++)
                {
                    for (int innerLookBack = 2; innerLookBack <= howFarToLookBack; innerLookBack++)
                    {
                        var indexToCheck1 = indexToCheck - outerLookBack;
                        var numToCheck1 = nums[indexToCheck1];
                        var indexToCheck2 = indexToCheck - innerLookBack;
                        var numToCheck2 = nums[indexToCheck2];
                        if (intToVerify == numToCheck1 + numToCheck2)
                        {
                            numberChecksOut = true;                            
                        }

                        //Console.WriteLine($"intToVerify {intToVerify}\t outerLookBack: {indexToCheck1}|{numToCheck1}\t innerLookBack: {indexToCheck2}|{numToCheck2}\t numberChecksOut {numberChecksOut}");

                        if (numberChecksOut)
                        {
                            break;                        
                        }
                    }

                    if (numberChecksOut)
                    {
                        break;
                    }
                }

                if (numberChecksOut)
                {
                    indexToCheck++;
                }
                //Console.WriteLine($"*********************************"); 

            } while (numberChecksOut);


            var invalidNumber = nums[indexToCheck];


            // Start Part 2
            var foundPart2Answer = false;
            var startedOn = 0;
            var minToTest = 0;
            var maxToTest = 1;
            var rangeToTest = new List<long>();
            do
            {
                do
                {
                    rangeToTest = nums.GetRange(minToTest, maxToTest - minToTest);
                    var sum = rangeToTest.Sum();

                    Console.WriteLine($"minToTest {minToTest}\t maxToTest: {maxToTest}\t sum: {sum}");

                    if (sum == invalidNumber)
                    {
                        foundPart2Answer = true;
                        break;
                    }

                    if (sum > invalidNumber)
                    {
                        break;
                    }

                    maxToTest++;

                } while (!foundPart2Answer);



                if (!foundPart2Answer)
                {
                    startedOn++;
                    minToTest = startedOn;
                    maxToTest = startedOn + 1;
                }

            } while (!foundPart2Answer);


            Console.WriteLine();
            return $"Part 1: {invalidNumber}, Part 2: {rangeToTest.Min() + rangeToTest.Max()}";
        
        }
    }
}