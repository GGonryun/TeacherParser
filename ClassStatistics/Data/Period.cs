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

        public Period(string yearSemesterCode)
        {
            if (yearSemesterCode.Length != 5)
            {
                throw new ArgumentException("SemesterYearCode should be in the format [YYYYS]!");
            }
            Year = Convert.ToInt32(yearSemesterCode.Substring(0, 4));
            Semester = (Semester)Convert.ToInt32(yearSemesterCode.Substring(4, 1));
        }

        public static string Code(Semester semester, int year)
        {
            return $"{year}{(int)semester}";
        }
    }
}
