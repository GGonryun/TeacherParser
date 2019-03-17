using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Class
{
    public class Course
    {
        public string Title { get; private set; }
        public string Subject { get; private set; }
        public int Number { get; private set; }
        public string Code { get => $"{Subject}-{Number}"; }

        public Course(string title, string subject, int number)
        {
            this.Title = title;
            this.Subject = subject;
            this.Number = number;
        }

        public Course(string title, string code)
        {
            this.Title = title;
            string[] parts = code.Split('-');
            Subject = parts[0];
            string parsedNumberOnly = Regex.Replace(parts[1], "[^0-9]+", string.Empty);
            Number = Convert.ToInt32(parsedNumberOnly);
        }



    }
}
