using System;

namespace AdventOfCode2020
{
    // https://adventofcode.com/2020/day/1
    public class Day1
    {
        public string Run()
        {
            var data1 = InputHelper.ReadOutEachLine("Day1Input");
            var data2 = InputHelper.ReadOutEachLine("Day1Input");
            var data3 = InputHelper.ReadOutEachLine("Day1Input");
            int? answer = null;

            foreach(var row1 in data1)
            {
                var num1 = int.Parse(row1);
                foreach (var row2 in data2)
                {
                    var num2 = int.Parse(row2);
                    foreach (var row3 in data3)
                    {
                        var num3 = int.Parse(row3);


                        if (num1 + num2 + num3 == 2020)
                        {
                            answer = num1 * num2 * num3;
                        }

                        if (answer != null)
                        {
                            break;
                        }

                    }


                    if(answer != null)
                    {
                        break;
                    }
                }

                if (answer != null)
                {
                    break;
                }
            }


            return answer.ToString();

        }
    }
}
