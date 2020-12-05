using System;

namespace AdventOfCode2020
{
    // https://adventofcode.com/2020/day/2
    public class Day2
    {
        public string Run()
        {
            var data1 = InputHelper.ReadOutEachLine("Day2Input");

            int goodMinMaxPassWord = 0;
            int goodPositionPassWord = 0;

            foreach (var codeRow in data1)
            {
                var dash = codeRow.IndexOf("-");
                var firstSpace = codeRow.IndexOf(" ");
                var colon = codeRow.IndexOf(":");
                var codePrefix = codeRow.IndexOf(": ");

                var minString = codeRow.Substring(0, dash);
                var maxString = codeRow.Substring(dash + 1, firstSpace- dash);

                var min = int.Parse(minString);
                var max = int.Parse(maxString);
                var letterToTest = codeRow.Substring(firstSpace + 1, 1);
                var codeToTest = codeRow.Substring(codePrefix + 2);


                var timesSeenLetter = 0;
                foreach(var letter in codeToTest)
                {
                    if(string.Equals(letter.ToString(), letterToTest))
                    {
                        timesSeenLetter++;
                    }
                }

                if (timesSeenLetter >= min && timesSeenLetter <= max)
                {
                    goodMinMaxPassWord++;
                }

                var letterAtMinPosition = codeToTest[min - 1].ToString();
                var letterAtMaxPosition = codeToTest[max - 1].ToString();
                var minIsRight = string.Equals(letterAtMinPosition, letterToTest);
                var maxIsRight = string.Equals(letterAtMaxPosition, letterToTest);


                if ((minIsRight || maxIsRight) && !(minIsRight && maxIsRight) )
                {
                    goodPositionPassWord++;
                }


            }

            return $"Part 1: {goodMinMaxPassWord}, Part 2: {goodPositionPassWord}";
        }
    }
}
