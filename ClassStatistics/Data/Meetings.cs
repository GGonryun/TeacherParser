using System;
using System.Collections.Generic;
using System.Text;

namespace Class
{
    class Meetings
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

        public void Filter()
        {

        }

    }
}
