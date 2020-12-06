using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020
{
    // https://adventofcode.com/2020/day/6
    public class Day6
    {
        public string Run()
        {
            var fullInput = InputHelper.ReadOutEachLine("Day6Input");

            var groups = new List<Group>();


            var currentGroup = new Group();


            for(int inputRow = 0; inputRow < fullInput.Count; inputRow++ )
            {
                if(fullInput[inputRow] == string.Empty)
                {
                    groups.Add(currentGroup);
                    Console.WriteLine(currentGroup.ToString());

                    currentGroup = new Group();
                    continue;
                }

                var yesAnswers = new List<char>(fullInput[inputRow]);

                currentGroup.People.Add(new Person { YesAnswers = yesAnswers });

                // last line...
                if (inputRow == fullInput.Count - 1)
                {
                    groups.Add(currentGroup);

                    Console.WriteLine(currentGroup.ToString());
                }
            }

            Console.WriteLine("************************");
            Console.WriteLine();
            return $"Part 1: {groups.Sum(x => x.AnyoneSaidYesCount())}, Part 2: {groups.Sum(x => x.EveryoneSaidYesCount())}";
        }

        private class Group
        {
            public IList<Person> People { get; set; } = new List<Person>();

            private int HasLetter(char letter) => People.Any(x => x.YesAnswers.Contains(letter)) ? 1 : 0;
            private int AllHasLetter(char letter) => People.All(x => x.YesAnswers.Contains(letter)) ? 1 : 0;

            private IList<char> Letters { get; } = new List<char>{ 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };

            public int AnyoneSaidYesCount()
            {
                var count = 0;
                foreach(var letter in this.Letters)
                {
                    count += this.HasLetter(letter);
                }

                return count;
            }

            public int EveryoneSaidYesCount()
            {
                var count = 0;
                foreach (var letter in this.Letters)
                {
                    count += this.AllHasLetter(letter);
                }

                return count;
            }

            public override string ToString() => $"People = {string.Join(",", this.People)}, AnyoneSaidYesCount: {this.AnyoneSaidYesCount()}, EveryoneSaidYesCount: {this.EveryoneSaidYesCount()}" ;
        }

        private class Person
        {
            public IList<char> YesAnswers { get; set; } = new List<char>();

            public override string ToString() => new string(this.YesAnswers.ToArray());
        }
    }
}
