using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020
{
    // https://adventofcode.com/2020/day/5
    public class Day5
    {
        public string Run()
        {
            var fullInput = InputHelper.ReadOutEachLine("Day5Input");

            var seats = new List<Seat>();

            foreach(var seatInput in fullInput)
            {
                var seat = new Seat {SeatInput = seatInput };


                int lowerRow = 0;
                int upperRow = 127;

                for (int rowCounter = 0; rowCounter < 7; rowCounter++)
                {
                    var inputItem = seat.SeatInput[rowCounter];

                    //Console.WriteLine($"Before:  Full Input: {seatInput}, rowCounter: {rowCounter}, rowInput: {inputItem}, lowerRow: {lowerRow}, upperRow: {upperRow}, ");

                    if(inputItem == 'B')
                    {
                        var half = ((double)upperRow - (double)lowerRow)/2;
                        lowerRow += (int)Math.Ceiling((double)half);
                    }
                    else
                    {
                        var half = ((double)upperRow - (double)lowerRow)/ 2;
                        upperRow -= (int)Math.Ceiling((double)half);
                    }

                    //Console.WriteLine($"After: Full Input: {seatInput}, rowCounter: {rowCounter}, rowInput: {inputItem}, lowerRow: {lowerRow}, upperRow: {upperRow}, ");
                    //Console.WriteLine("***");

                }

                // both should be the same now
                seat.Row = upperRow;


                int lowerColumn = 0;
                int upperColumn = 7;
                for (int columnCounter = 7; columnCounter < 10; columnCounter++)
                {
                    var inputItem = seat.SeatInput[columnCounter];

                    //Console.WriteLine($"Before:  Full Input: {seatInput}, columnCounter: {columnCounter}, rowInput: {inputItem}, lowerRow: {lowerColumn}, upperRow: {upperColumn}, ");

                    if (inputItem == 'R')
                    {
                        var half = ((double)upperColumn - (double)lowerColumn) / 2;
                        lowerColumn += (int)Math.Ceiling((double)half);
                    }
                    else
                    {
                        var half = ((double)upperColumn - (double)lowerColumn) / 2;
                        upperColumn -= (int)Math.Ceiling((double)half);
                    }

                    //Console.WriteLine($"After: Full Input: {seatInput}, columnCounter: {columnCounter}, rowInput: {inputItem}, lowerRow: {lowerColumn}, upperRow: {upperColumn}, ");
                    //Console.WriteLine("***");

                }

                // both should be the same now
                seat.Column = upperColumn;


                //Console.WriteLine(seat.ToString());
                //Console.WriteLine("************************");
                //Console.WriteLine(); 

                seats.Add(seat);

            }




            var orderedSeats = seats.OrderBy(x => x.Row).ThenBy(x => x.Column);
            var priorSeatNumber = 0;
            var yourSeatNumber = 0;

            foreach(var seat in orderedSeats)
            {
                Console.WriteLine(seat.ToString());

                var currentSeatNumber = seat.SeatNumber();         

                if(priorSeatNumber !=  0 && currentSeatNumber > priorSeatNumber + 1)
                {
                    yourSeatNumber = priorSeatNumber + 1;

                }
                priorSeatNumber = seat.SeatNumber();
            }

            Console.WriteLine("************************");
            Console.WriteLine(); 
            return $"Part 1: {seats.Max(x => x.SeatNumber() )}, Part 2: {yourSeatNumber}";
        }


        private class Seat
        {
            public string SeatInput { get; set; }

            public int Row { get; set; }

            public int Column { get; set; }

            public int SeatNumber() => this.Row * 8 + this.Column;

            public override string ToString() => $"SeatInput: {SeatInput}; Row: {Row}; Column: {Column}; SeatNumber: {SeatNumber()}; ";

        }

    }
}
