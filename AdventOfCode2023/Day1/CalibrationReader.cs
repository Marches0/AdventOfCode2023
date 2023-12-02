using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day1
{
    internal class CalibrationReader
    {
        public int ReadLine(string line)
        {
            // Value = first & last digit combined
            // One digit means that one is both first and last
            return int.Parse(
                line.First(char.IsDigit).ToString()
              + line.Last(char.IsDigit).ToString()
            );
        }
    }
}