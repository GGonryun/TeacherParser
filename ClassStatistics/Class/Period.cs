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

        public static bool operator <(Period p1, Period p2)
        {
            if(p1.Year < p2.Year)
            {
                return true;
            }
            else if(p1.Year == p2.Year && (int)p1.Semester < (int)p2.Semester) 
            {
                return true;
            }
            return false;

        }
        public static bool operator >(Period p1, Period p2)
        {
            if (p1.Year > p2.Year)
            {
                return true;
            }
            else if (p1.Year == p2.Year && (int)p1.Semester > (int)p2.Semester)
            {
                return true;
            }
            return false;
        }

        public static Period operator ++(Period p1)
        {
            int year = p1.Year;
            int semester = (int)p1.Semester;

            semester += 1;
            if(semester > 4)
            {
                year++;
                semester = 1;
            }
            return new Period(semester, year);
        }

        public string Code()
        {
            return $"{Year}{(int)Semester}";
        }
    }
}
