using System;
using System.Collections.Generic;
using Class;
using Filtering;
using Filtering.Specifications;
namespace ClassStatistics
{

    public class Program
    {
        static void Main(string[] args)
        {
            SdsuMeetingsBuilder builder = new SdsuMeetingsBuilder("BIOL", "20182", "20183", "20184");
            Meetings meetings = builder.GetResult();
            IFilter<Meeting> filter = new MatchAllFilter<Meeting>();

            List<ISpecification<Meeting>> specs = new List<ISpecification<Meeting>>();
            specs.Add(new TimeNonOverlappingSpecification(new Time("1200-1600")));
            var filteredMeetings = filter.Filter(meetings, specs.ToArray());

            foreach(Meeting meeting in filteredMeetings)
            {
                Console.WriteLine(meeting.ToString());
            }
        }
    }

   
}
