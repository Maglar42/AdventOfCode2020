using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode2020
{
    // https://adventofcode.com/2020/day/11
    public class Day11
    {
        private List<Location> layout = new List<Location>();

        private List<Location> priorLayout = new List<Location>();
        private int MaxXcord => this.layout.Max(x => x.Xcord);
        private int MaxYcord => this.layout.Max(x => x.Ycord);

        public string Run()
        {
            var fullInput = InputHelper.ReadOutEachLine("Day11Input");

            for (int row = 0; row < fullInput.Count(); row++)
            {
                for (int item = 0; item < fullInput[row].Length; item++)
                {
                    var location = new Location { Xcord = item , Ycord = row };
                    var charToAdd = fullInput[row][item];
                    switch (charToAdd)
                    {
                        case ('L'):
                            location.Occupied = false;
                            break;
                        case ('#'):
                            location.Occupied = true;
                            break;
                        case ('.'): // not really needed...
                            location.Occupied = null;
                            break;
                    }
                    layout.Add(location);
                }
            }

            //this.Print();
            do
            {
                this.UpdateSeats();
                this.Print();
                //Console.ReadLine();
            } while (!this.Compare(priorLayout));

                //Console.WriteLine("************************");
                Console.WriteLine();
            return $"Part 1: { this.priorLayout.Count(x => x.Occupied == true) }, Part 2: {"TBD"}";
        }

        private bool Compare(IList<Location> loctions)
        {
            var string1 = string.Join("", loctions.OrderBy(x => x.Ycord).ThenBy(x => x.Xcord));
            var string2 = string.Join("", this.layout.OrderBy(x => x.Ycord).ThenBy(x => x.Xcord));
            var same = string1 == string2;
            return same;
        }


        private void Print()
        {
            //for(int counter = 0; counter <= this.MaxXcord; counter++)
            //{
            //    var row = "";
            //    var priorRowItems = this.FindRow(this.priorLayout, counter);
            //    row += string.Join("", priorRowItems);

            //    row += " ";

            //    var rowItems = this.FindRow(this.layout, counter);
            //    row += string.Join("", rowItems);

            //    Console.WriteLine(row);
            //}

            //Console.WriteLine("");

            //for (int counter = 0; counter <= this.MaxXcord; counter++)
            //{
            //    var row = "";

            //    var priorRowItems = this.FindRow(this.priorLayout, counter);    
            //    foreach (var item in priorRowItems)
            //    {
            //        row += CountSeats(item, this.priorLayout);
            //    }

            //    row += " ";
            //    var rowItems = this.FindRow(this.layout, counter);            
            //    foreach (var item in rowItems)
            //    {
            //        row += CountSeats(item, this.layout);
            //    }

            //    Console.WriteLine(row);
            //}

            Console.WriteLine("************************");
            Console.WriteLine(this.layout.Count(x => x.Occupied == true));
            Console.WriteLine("************************");
        }

        private IList<Location> FindRow(IList<Location> locations, int row) => locations.Where(item => item.Ycord == row).OrderBy(x => x.Xcord).ToList();

        private void UpdateSeats()
        {
            this.priorLayout = new List<Location>();
            foreach (var item in this.layout)
            {
                this.priorLayout.Add(new Location { Xcord = item.Xcord, Ycord = item.Ycord, Occupied = item.Occupied });
            }

            this.layout = new List<Location>();
            foreach (var location in this.priorLayout)
            {
                var seatCount = this.CountSeats(location, this.priorLayout);
                if (location.Occupied == true && seatCount >= 4)
                {
                    this.layout.Add(new Location { Xcord = location.Xcord, Ycord = location.Ycord, Occupied = false });
                }
                else if(location.Occupied == false && seatCount == 0)
                {
                    this.layout.Add(new Location { Xcord = location.Xcord, Ycord = location.Ycord, Occupied = true });
                }
                else // if no change or empty spot
                {                    
                    this.layout.Add(new Location { Xcord = location.Xcord, Ycord = location.Ycord, Occupied = location.Occupied });
                }
            }
        }

        private int CountSeats(Location location, IList<Location> locationToCheckAgainst)
        {
            if(location.Occupied == null)
            {
                return 0;
            }

            // the names are wrong...
            var topLeft = this.CountIfOccupied(locationToCheckAgainst, location.Xcord - 1, location.Ycord - 1); // top Left
            var topCenter = this.CountIfOccupied(locationToCheckAgainst, location.Xcord - 1, location.Ycord + 0); // top center
            var topRight = this.CountIfOccupied(locationToCheckAgainst, location.Xcord - 1, location.Ycord + 1); // top right

            var centerLeft = this.CountIfOccupied(locationToCheckAgainst, location.Xcord + 0, location.Ycord - 1); // center right
            var centerRight = this.CountIfOccupied(locationToCheckAgainst, location.Xcord + 0, location.Ycord + 1); // center left

            var bottomLeft = this.CountIfOccupied(locationToCheckAgainst, location.Xcord + 1, location.Ycord - 1); // bottom left
            var bottomCenter = this.CountIfOccupied(locationToCheckAgainst, location.Xcord + 1, location.Ycord + 0); // top center
            var bottomRight = this.CountIfOccupied(locationToCheckAgainst, location.Xcord + 1, location.Ycord + 1); // bottom right

            var occupied =  topLeft + topCenter + topRight + centerLeft + centerRight + bottomLeft + bottomCenter + bottomRight;

            //if(occupied >=3)
            //{
            //    _ = "breakhere";
            //}

            return occupied;
        }

        private int CountIfOccupied(IList<Location> locationToCheckAgainst, int xcord, int ycord)
        {
            var location = locationToCheckAgainst.FirstOrDefault(x => x.Xcord == xcord && x.Ycord == ycord);
            if(location == null)
            {
                return 0;
            }

            return location.Occupied == true ? 1 : 0;
        }

        private class Location
        {
            public int Xcord { get; set; }
            public int Ycord { get; set; }

            // Null means floor 
            public bool? Occupied { get; set; }

            public override string ToString()
            {
                if (Occupied == true)
                {
                    return "#";
                }

                if (Occupied == false)
                {
                    return "L";
                }

                return ".";
     
            }
        }
    }
}