using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2020
{
    public static class Helper
    {
        public static void  WriteLine(IList<KeyValuePair<string, string>> toWrite)
        {
            var stringBuilder = new StringBuilder();
            foreach(var item in toWrite)
            {
                stringBuilder.Append($"{item.Key} {item.Value}\t");
            }

            Console.WriteLine(stringBuilder.ToString());
        }

        public static void WriteLine(IList<string> toWrite)
        {
            var stringBuilder = new StringBuilder();
            foreach (var item in toWrite)
            {
                stringBuilder.Append($"{item}\t");
            }

            Console.WriteLine(stringBuilder.ToString());
        }

    }
}
