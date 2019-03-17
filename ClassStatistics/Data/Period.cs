using System;
using System.Collections.Generic;
using System.Text;
using Utility;
namespace Class
{
    public class Period
    {
        public Semester Semester { get; private set; }
        public int Year { get; private set; }
        
        public Period(Semester semester, int year)
        {
            this.Semester = semester;
            this.Year = year;
        }

        public Period(int semester, int year) : this((Semester)semester, year)
        {
        }

        public static string Code(Semester semester, int year)
        {
            return $"{year}{(int)semester}";
        }
    }
}
