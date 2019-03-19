using NodaTime;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using static NodaTime.IsoDayOfWeek;
namespace Class
{
    public class Day : IEnumerable<IsoDayOfWeek>
    {
        HashSet<IsoDayOfWeek> days;
        public string Code
        {
            get
            {
                StringBuilder daysOfTheWeek = new StringBuilder();
                foreach(IsoDayOfWeek day in this)
                {
                    daysOfTheWeek.Append(ConvertToCode(day));
                }
                return daysOfTheWeek.ToString();
            }
        }
        string ConvertToCode(IsoDayOfWeek day)
        {
            switch (day)
            {
                case Monday:
                    return "M";
                case Tuesday:
                    return "T";
                case Wednesday:
                    return "W";
                case Thursday:
                    return "TH";
                case Friday:
                    return "F";
                case Saturday:
                    return "S";
                default:
                    throw new System.ArgumentException($"{day.ToString()} is not a valid school day!");
            }
        }
        public HashSet<IsoDayOfWeek> Days
        {
            get => days;
        }

        Day()
        {
            days = new HashSet<IsoDayOfWeek>();
        }

        public Day(string code) : this()
        {
            CodeToDaysOfWeek(code);
        }


        void CodeToDaysOfWeek(string code)
        {
            if(code.Equals("Arranged"))
            {
                return;
            }
            Stack<IsoDayOfWeek> stack = new Stack<IsoDayOfWeek>();
            for (int i = code.Length - 1; i >= 0; i--)
            {
                switch (code[i])
                {
                    case 'M':
                        stack.Push(Monday);
                        break;
                    case 'T':
                        stack.Push(Tuesday);
                        break;
                    case 'W':
                        stack.Push(Wednesday);
                        break;
                    case 'H':
                        stack.Push(Thursday);
                        i--;
                        break;
                    case 'F':
                        stack.Push(Friday);
                        break;
                    case 'S':
                        stack.Push(Saturday);
                        break;
                }
            }
            for(int i = 0, j = stack.Count; i < j; i++)
            {
                days.Add(stack.Pop());
            }
        }

        public IEnumerator<IsoDayOfWeek> GetEnumerator()
        {
            foreach(IsoDayOfWeek day in days)
            {
                yield return day;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
