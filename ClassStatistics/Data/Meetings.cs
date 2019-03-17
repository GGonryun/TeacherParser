using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Class
{
    public class Meetings : IEnumerable<Meeting>
    {
        List<Meeting> meetings;

        public Meetings()
        {
            meetings = new List<Meeting>();
        }

        public void Add(Meeting meeting)
        {
            meetings.Add(meeting);
        }

        public void Add(List<Meeting> meetings)
        {
            meetings.AddRange(meetings);
        }

        public IEnumerator<Meeting> GetEnumerator()
        {
            foreach(Meeting meeting in meetings)
            {
                yield return meeting;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator() as IEnumerator;
        }
    }
}
