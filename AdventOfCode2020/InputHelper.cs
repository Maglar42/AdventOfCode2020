using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace AdventOfCode2020
{
    public static class InputHelper
    {
        public static IList<string> ReadOutEachLine(string fileNameWithOutTxt)
        {
            // make sure to set the file to be content
            // https://stackoverflow.com/questions/13762338/read-files-from-a-folder-present-in-project
            var path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), $"Inputs\\{fileNameWithOutTxt}.txt");
            var data = File.ReadAllLines(path);
            return data;
        }



    }
}
