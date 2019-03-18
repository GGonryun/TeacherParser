using System;
using System.Collections.Generic;
using Class;
using Filtering;
using Filtering.Specifications;
using Utility;

namespace ClassStatistics
{
    public enum School { SDSU }
    public class Program
    {
        static void Main(string[] args)
        {
            School school = School.SDSU;

            List<ISpecification<Meeting>> specs = new List<ISpecification<Meeting>>();
            List<string> subjects = new List<string>();
            List<Period> periods = new List<Period>();

            for (int i = 0; i < args.Length; i++)
            {
                switch (args[i])
                {
                    //=== SUBJECT ARGUMENTS ===//
                    case "--s":
                        i++;
                        subjects.Add(args[i]);
                        break;
                    case "--sm":
                        i++;
                        while (args[i] != null && (args[i])[0] != '-')
                        {
                            subjects.Add(args[i++]);
                        }
                        i--;
                        break;
                    //=== PERIOD ARGUMENTS ===//
                    case "--p":
                        i++;
                        periods.Add(new Period(args[i]));
                        break;
                    case "--pr":
                        i++;
                        string[] range = args[i].Split('-');
                        Period start = new Period(range[0]);
                        Period end = new Period(range[1]);
                        end++;
                        for (Period s = start; s < end; s++)
                        {
                            periods.Add(s);
                        }
                        break;
                    case "--pm":
                        i++;
                        while (args[i] != null && (args[i])[0] != '-')
                        {
                            periods.Add(new Period(args[i]));
                            i++;
                        }
                        i--;
                        break;
                    //=== FILL RATIO ARGUMENTS ===//
                    case "--maxRatio":
                        i++;
                        float maxR = float.Parse(args[i]);
                        specs.Add(new ClassRatioSpecification(ratio => ratio <= maxR));
                        break;
                    case "--minRatio":
                        i++;
                        float minR = float.Parse(args[i]);
                        specs.Add(new ClassRatioSpecification(ratio => ratio >= minR));
                        break;
                    //=== LEVEL ARGUMENTS ===//
                    case "--maxLevel":
                        i++;
                        int maxL = Convert.ToInt32(args[i]);
                        specs.Add(new CourseLevelSpecification(level => level <= maxL));
                        break;
                    case "--minLevel":
                        i++;
                        int minL = Convert.ToInt32(args[i]);
                        specs.Add(new CourseLevelSpecification(level => level >= minL));
                        break;
                }
            }

            IBuilder<Meetings> builder = GetBuilder(school, subjects, periods);
            Meetings meetings = builder.GetResult();

            IFilter<Meeting> filter = new MatchAllFilter<Meeting>();
            var filteredMeetings = filter.Filter(meetings, specs.ToArray());

            foreach (Meeting meeting in filteredMeetings)
            {
                Console.WriteLine(meeting.ToString());
            }
        }

        public static IBuilder<Meetings> GetBuilder(School school, List<string> subjects, List<Period> periods)
        {
            switch (school)
            {
                case School.SDSU:
                    return new SdsuMeetingsBuilder(subjects, periods);
            }
            return null;
        }
    }


   


}
