using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace AdventOfCode2020
{
    // https://adventofcode.com/2020/day/4
    // Note only solveing part 2 right now.
    public class Day4
    {
        private IList<string> data;

        private IList<Dictionary<string, string>> passports = new List<Dictionary<string, string>>();

        private int vaildPassports = 0;

        // There was no trailing blank line, so i added one...
        public string Run()
        {
            this.data = InputHelper.ReadOutEachLine("Day4Input");
            var currentItem = new Dictionary<string, string>();

            for(int rowNumber = 0; rowNumber < data.Count; rowNumber++)
            {
                if (data[rowNumber] == string.Empty)
                {
                    this.ReviewPassport(currentItem);
                    currentItem = new Dictionary<string, string>();
                    continue;
                }

                foreach (var rowItem in data[rowNumber].Split(" "))
                {
                    var subItem = rowItem.Split(":");
                    currentItem[subItem[0]] = subItem[1];
                }
                
                // "if" the last row is not a blank line...
                if(rowNumber == data.Count - 1)
                {
                    this.ReviewPassport(currentItem);
                    currentItem = new Dictionary<string, string>();
                }

            }

            return vaildPassports.ToString();
        }

        private void ReviewPassport(Dictionary<string, string> currentItem)
        {
            bool vaild = true;

            // byr (Birth Year) - four digits; at least 1920 and at most 2002.
            if (!currentItem.TryGetInt("byr", out var birthYear) || birthYear < 1920 || birthYear > 2002)
            {
                vaild = false;
            }

            // iyr (Issue Year) - four digits; at least 2010 and at most 2020.
            if (!currentItem.TryGetInt("iyr", out var issueYear) || issueYear < 2010 || issueYear > 2020)
            { 
                vaild = false;
            }

            // eyr (Expiration Year) - four digits; at least 2020 and at most 2030.
            if (!currentItem.TryGetInt("eyr", out var experationYear) || experationYear < 2020 || experationYear > 2030)
            {
                vaild = false;
            }

            //* hgt (Height) - a number followed by either cm or in
            if (currentItem.TryGetValue("hgt", out var heightString))
            {
                //* If cm, the number must be at least 150 and at most 193.
                if (heightString.Contains("cm"))
                {
                    if (int.TryParse(heightString.Replace("cm", ""), out var heightInCm))
                    {
                        if(heightInCm < 150 || heightInCm > 193)
                        {
                            vaild = false;
                        }
                    }
                    else
                    {
                        vaild = false;
                    }
                }
                else
                {
                    //* If in, the number must be at least 59 and at most 76.
                    if (int.TryParse(heightString.Replace("in", ""), out var heightInIn))
                    {
                        if (heightInIn < 59 || heightInIn > 76)
                        {
                            vaild = false;
                        }
                    }
                    else
                    {
                        vaild = false;
                    }
                }

            }
            else
            {
                vaild = false;
            }

            //* hcl (Hair Color) - a # followed by exactly six characters 0-9 or a-f.
            Regex hairColorRegex = new Regex(@"^[#]{1}[0-9a-f]{6}$");
            if (!currentItem.TryGetValue("hcl", out var hairColor) || !hairColorRegex.IsMatch(hairColor))
            {
                vaild = false;
            }

            //* ecl (Eye Color) - exactly one of: amb blu brn gry grn hzl oth.
            var validEyes = new List<string> { "amb", "blu", "brn", "gry", "grn", "hzl", "oth" };
            if (!currentItem.TryGetValue("ecl", out var eyeColor) || !validEyes.Contains(eyeColor))
            {
                vaild = false;
            }

            //* pid (Passport ID) - a nine-digit number, including leading zeroes.
            Regex passportIdRegex = new Regex(@"^[0-9]{9}$");
            if(!currentItem.TryGetValue("pid", out var passPortId) || !passportIdRegex.IsMatch(passPortId))
            {
                vaild = false;
            }

            this.vaildPassports += vaild ? 1 : 0;

            this.passports.Add(currentItem);
        }




    }
}
