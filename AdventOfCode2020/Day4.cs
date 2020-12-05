using System;
using System.Collections.Generic;

namespace AdventOfCode2020
{
    // https://adventofcode.com/2020/day/1
    public static class Day4
    {
        public static string Run()
        {

            var data = InputHelper.ReadOutEachLine("Day4Input");

            var items = new List<Dictionary<string, string>>();
            var currentItem = new Dictionary<string, string>();

            var vaildPassports = 0;

            foreach(var row in data)
            {
                if (row == string.Empty)
                {

                    if(currentItem.Count == 8 || (currentItem.Count == 7 && !currentItem.ContainsKey("cid")))
                    {
                        vaildPassports++;
                    }
                    items.Add(currentItem);
                    currentItem = new Dictionary<string, string>();

                    continue;
                }


                var rowItems = row.Split(" ");

                foreach(var rowItem in rowItems)
                {
                    var subItem = rowItem.Split(":");
                    currentItem[subItem[0]] = subItem[1];
                }
            }



            return vaildPassports.ToString();
        }
    }
}
