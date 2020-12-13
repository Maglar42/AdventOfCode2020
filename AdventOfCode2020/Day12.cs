using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode2020
{
    // https://adventofcode.com/2020/day/8
    public class Day12
    {
        private int part1xCord = 0;
        private int part1yCord = 0;
        private int part1Direction = 90;

        private int part2xCord = 0;
        private int part2yCord = 0;

        private int waypointxSteps = 1;
        private int waypointySteps = 10;

        public string Run()
        {
            var fullInput = InputHelper.ReadOutEachLine("Day12Input");
            var instructions = new List<KeyValuePair<char, int>>();
            
            int direction = 90;

            foreach (var item in fullInput)
            {
                var rule = item[0];
                var steps = int.Parse(item.Substring(1).Trim());
                instructions.Add(new KeyValuePair<char, int>(rule, steps));

                switch (rule)
                {
                    case 'L':
                        this.TurnAndMoveWayPoint(false, steps);
                        break;
                    case 'R':
                        this.TurnAndMoveWayPoint(true, steps);
                        break;
                    case 'F':
                        for(int step = 0; step < steps; step ++ )
                        {
                            part2xCord += waypointxSteps;
                            part2yCord += waypointySteps;
                        }
                        
                        this.Step(direction, steps);
                        break;
                    case 'N':
                        waypointxSteps += steps;
                        this.Step(0, steps);
                        break;
                    case 'S':
                        waypointxSteps -= steps;
                        this.Step(180, steps);
                        break;
                    case 'E':
                        waypointySteps += steps;
                        this.Step(90, steps);
                        break;
                    case 'W':
                        waypointySteps -= steps;
                        this.Step(270, steps);
                        break;

                }

                Helper.WriteLine( new List<string> 
                { 
                    item,
                    $"rule  {rule}",
                    $"steps  {steps}",
                    //$"xCord  {this.part1xCord}",
                    //$"yCord  {this.part1yCord}",
                    //$"direction  {direction}",
                    $"part2xCord  {this.part2xCord}",
                    $"part2yCord  {this.part2yCord}",
                    $"waypointxSteps  {this.waypointxSteps}",
                    $"waypointySteps  {this.waypointySteps}",
                });

            }    

            //Console.WriteLine("************************");
            Console.WriteLine();
            return $"Part 1: { Math.Abs(part1xCord) + Math.Abs(part1yCord) }, Part 2: {Math.Abs(part2xCord) + Math.Abs(part2yCord)}";
        }

        private void Step(int direction, int steps)
        {
            switch (direction)
            {
                case 0:
                    this.part1xCord += steps;
                    break;
                case 180:
                    this.part1xCord -= steps;
                    break;
                case 90:
                    this.part1yCord += steps;
                    break;
                case 270:
                    this.part1yCord -= steps;
                    break;

            }
        }

        private void TurnAndMoveWayPoint(bool turnRight, int degreesToStep)
        {
            var realDegreeSteps = degreesToStep % 360;

            if (!turnRight)
            {
                // Silly but easier to think about...
                switch (degreesToStep)
                {
                    case 90:
                        realDegreeSteps = 270;
                        break;
                    case 270:
                        realDegreeSteps = 90;
                        break;
                    case 0:
                    case 360:
                    case 180:
                    default:
                        break;
                }
            }

            realDegreeSteps = realDegreeSteps % 360;

            this.part1Direction = (part1Direction + realDegreeSteps) % 360;

            switch (realDegreeSteps)
            {
                case 90:
                    this.RotateWayPointBy90(1);
                    break;
                case 180:
                    this.RotateWayPointBy90(2);
                    break;
                case 270:
                    this.RotateWayPointBy90(3);
                    break; 

                case 360:
                case 0:
                default:
                    break;
            }
        }

        private void RotateWayPointBy90(int times)
        {
            for(int time = 0; time < times; time++)
            {
                var temp = this.waypointxSteps;
                this.waypointxSteps = -this.waypointySteps;
                this.waypointySteps = temp;
            }
        }

    }
}