using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode2020
{
    // https://adventofcode.com/2020/day/7
    public class Day07
    {
        public string Run()
        {
            var fullInput = InputHelper.ReadOutEachLine("Day7Input");

            var bagRules = new List<BagRule>();

            foreach (var inputRow in fullInput)
            {
                var bagRule = new BagRule();
                var inputNoBagString = inputRow.Replace("bags", string.Empty ).Replace("bag", string.Empty).Replace(".", string.Empty);

                var splitOnContains = inputNoBagString.Split("contain ");
                bagRule.BagType = splitOnContains[0].Trim();

                if(!splitOnContains[1].Contains("no other"))
                {
                    var holds = splitOnContains[1].Split(",");
                    foreach (var held in holds)
                    {
                        var cleanHeld = held.Trim();
                        var firstSpace = cleanHeld.IndexOf(" ");
                        var numberAsString = cleanHeld.Substring(0, firstSpace).Trim();
                        var number = int.Parse(numberAsString);
                        var bagName = cleanHeld.Substring(firstSpace).Trim();
                        bagRule.Holds.Add(new KeyValuePair<string, int>(bagName, number));
                    }
                }
                bagRules.Add(bagRule);

                //Console.WriteLine($"******* {bagRule} ");
            }

            int holdsShinyGold = 0;
            foreach(var rule in bagRules)
            {
                holdsShinyGold += rule.HoldsType("shiny gold", bagRules) ? 1: 0;
                //Console.WriteLine($"Counter = {holdsShinyGold} after: {rule}");
            }

            var shinyBagRule = bagRules.First(x => x.BagType == "shiny gold");
            var inShiny = shinyBagRule.BagsInThisBag(bagRules) - 1 ; // Don't count the shiny gold bag

            //Console.WriteLine("************************");
            Console.WriteLine();
            return $"Part 1: {holdsShinyGold}, Part 2: {inShiny}";
        }
        private class BagRule
        {
            public string BagType { get; set; }

            public IList<KeyValuePair<string, int>> Holds { get; set; } = new List<KeyValuePair<string, int>>();

            public int BagsInThisBag(IList<BagRule> allRules)
            {
                int bags = 0;
                foreach(var held in this.Holds)
                {
                    var heldRule = allRules.First(x => x.BagType == held.Key);
                    var bagsInheld = heldRule.BagsInThisBag(allRules);
                    if(bagsInheld == 0)
                    {
                        bags += held.Value;
                        //Console.WriteLine($"heldRule {heldRule.BagType} adds {held.Value}");
                    }
                    else
                    {
                        bags += held.Value * bagsInheld;
                        //Console.WriteLine($"heldRule {heldRule.BagType} adds {held.Value * bagsInheld}");
                    }
                }

                Console.WriteLine($"heldRule {BagType} adds {bags}");

                return bags + 1;
            } 


            public bool HoldsType(string typeToCheck, IList<BagRule> allRules)
            {                
                if(this.Holds.Any(x => x.Key == typeToCheck))
                {
                    return true;
                }


                foreach(var held in this.Holds)
                {
                    var rule = allRules.First(x => x.BagType == held.Key);
                    if (rule.HoldsType(typeToCheck, allRules))
                    {
                        return true;
                    }

                }

                return false;
            }



            public override string ToString() 
            {
                var builder = new StringBuilder();
                builder.Append($"BagType: {BagType} --> Holds: ");

                foreach(var held in this.Holds)
                {
                    builder.Append($"{held.Value} {held.Key}, ");
                }
                    

                return builder.ToString();


            }

        }
    }
}