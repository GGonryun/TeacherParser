using System;
using System.Collections.Generic;
using System.Text;

namespace Class
{
    public class Course
    {
        public string Subject { get; private set; }
        public int Number { get; private set; }

        public Course(string subject, int number)
        {
            this.Subject = subject;
            this.Number = number;
        }

        public override string ToString()
        {
            return $"{Subject}-{Number}";
        }
    }
}
