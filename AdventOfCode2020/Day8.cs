using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode2020
{
    // https://adventofcode.com/2020/day/8
    public class Day8
    {
        public string Run()
        {
            var rules = this.BuildRules();
            this.CheckRuleSet(rules, out var part1Answer);


            int part2Answer = 0;
            bool foundAnswer;

            for (int ruleIndex = 0; ruleIndex< rules.Count(); ruleIndex++)
            {
                var copyOfRules = this.BuildRules();

                if (copyOfRules[ruleIndex].ActionType == "acc")
                {
                    continue;
                }

                if(copyOfRules[ruleIndex].ActionType == "jmp")
                {
                    copyOfRules[ruleIndex].ActionType = "nop";
                }
                else
                {
                    copyOfRules[ruleIndex].ActionType = "jmp";
                }

                Console.WriteLine("*************************************");
                Console.WriteLine($"Trying updating {ruleIndex} to {copyOfRules[ruleIndex].ActionType}");
                foundAnswer = this.CheckRuleSet(copyOfRules, out part2Answer);
                Console.WriteLine($"foundAnswer {foundAnswer}; part2Answer {part2Answer}");
                Console.WriteLine("*************************************");
                if (foundAnswer)
                {
                    break;
                }

            } 

            //Console.WriteLine("************************");
            Console.WriteLine();
            return $"Part 1: {part1Answer}, Part 2: {part2Answer}";
        }

        private IList<Rule> BuildRules()
        {
            var fullInput = InputHelper.ReadOutEachLine("Day8Input");

            var rules = new List<Rule>();
            foreach (var inputRow in fullInput)
            {
                var rule = new Rule();
                var parts = inputRow.Split(" ");
                rule.ActionType = parts[0].Trim();
                rule.ActionAmount = int.Parse(parts[1]);

                //Console.WriteLine($"inputRow: {inputRow} ; ActionType = {rule.ActionType}; ActionAmount: {rule.ActionAmount }");
                rules.Add(rule);
            }

            return rules;
        }


        private bool CheckRuleSet(IList<Rule> rules, out int accumulator)
        {
            var ruleRowNumber = 0;
            accumulator = 0;
            var foundAnswer = false;

            foreach(var rule in rules)
            {
                rule.Used = false;
            }

            do
            {
                var rule = rules[ruleRowNumber];
                Console.WriteLine($"ruleRowNumber {ruleRowNumber}\tActionType: {rule.ActionType}\tAmount: {rule.ActionAmount }\taccumulator {accumulator}");
                switch (rule.ActionType)
                {
                    case "acc":
                        accumulator += rule.ActionAmount;
                        ruleRowNumber++;
                        break;
                    case "jmp":
                        ruleRowNumber += rule.ActionAmount;
                        break;
                    case "nop":
                        ruleRowNumber++;
                        break;
                }

                rule.Used = true;
                if(ruleRowNumber >= rules.Count())
                {
                    foundAnswer = true;
                    break;
                }

            } while (foundAnswer || !rules[ruleRowNumber].Used);

            return foundAnswer;
        }

        private class Rule
        {
            public string ActionType { get; set; }

            public int ActionAmount { get; set; }

            public bool Used { get; set; }
        }
    }
}