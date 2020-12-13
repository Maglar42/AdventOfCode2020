using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020
{
    // https://adventofcode.com/2020/day/3
    public class Day03
    {
        public string Run()
        {
            var fullInput = InputHelper.ReadOutEachLine("Day3Input");
            var coursePattern = new List<List<bool>>();
            foreach(var row in fullInput)
            {
                var rowAsList = new List<bool>();
                foreach(var item in row)
                {
                    rowAsList.Add(string.Equals(item, '#'));
                }
                coursePattern.Add(rowAsList);
            }

            var slopes = new List<Slope>
            {
                new Slope{VertStep = 1, HorzStep = 1},
                new Slope{VertStep = 1, HorzStep = 3},
                new Slope{VertStep = 1, HorzStep = 5},
                new Slope{VertStep = 1, HorzStep = 7},
                new Slope{VertStep = 2, HorzStep = 1},
            };

            long treeMultiplier = 1;
            foreach (var slopeToCheck in slopes)
            {
                var currentRow = 0;
                var currentColumn = 0;
                var hitTrees = 0;
                do
                {
                    currentRow += slopeToCheck.VertStep;
                    currentColumn += slopeToCheck.HorzStep;
                    if (currentRow >= coursePattern.Count())
                    {
                        break;
                    }

                    var rowToCheck = coursePattern[currentRow];

                    if (currentColumn >= rowToCheck.Count)
                    {
                        // this looks wrong...
                        currentColumn -= rowToCheck.Count;
                    }

                    if (rowToCheck[currentColumn])
                    {
                        hitTrees++;
                    }

                }
                while (currentRow <= coursePattern.Count());

                slopeToCheck.Result = hitTrees;
                treeMultiplier *= hitTrees;
            }

            var day1Answer = slopes.FirstOrDefault(x => x.VertStep == 1 && x.HorzStep == 3).Result;
            return $"Part 1: {day1Answer}, Part 2: {treeMultiplier}";
        }




    }

    public class Slope
    {
        public int VertStep { get; set; }

        public int HorzStep { get; set; }

        public int Result { get; set; }
    }

}
