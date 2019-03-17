using System;
using System.Collections.Generic;
using Class;
using Filtering;
using Filtering.Specifications;
namespace ClassStatistics
{

    class Program
    {

        static void Main(string[] args)
        {
            SdsuMeetingsBuilder builder = new SdsuMeetingsBuilder("CS", "20182", "20183", "20184");
            Meetings meetings = builder.GetResult();
            IFilter<Meeting> filter = new MatchAllFilter<Meeting>();


            List<ISpecification<Meeting>> specs = new List<ISpecification<Meeting>>();
            specs.Add(new CourseLevelSpecification(Level.Junior, (x, y) => x > y));
            var filteredMeetings = filter.Filter(meetings, specs.ToArray());
        }
    }

   
}
