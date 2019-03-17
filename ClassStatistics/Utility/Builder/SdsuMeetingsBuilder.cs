using System;
using System.Collections.Generic;
using System.Text;
using Utility.HTML;
using Class;
using HtmlAgilityPack;
using Utility;

namespace ClassStatistics
{
    public class SdsuMeetingsBuilder : IBuilder<Meetings>
    {
        private Meetings _meetings;
        private readonly string[] _codes;
        private readonly string _majorAbbreviation;

        public SdsuMeetingsBuilder(string majorAbbreviation, params string[] codes)
        {
            _codes = codes;
            _majorAbbreviation = majorAbbreviation;
        }

        public Meetings GetResult()
        {
            if(_meetings == null)
            {
                _meetings = new Meetings();
                foreach (string code in _codes)
                {
                    Fetcher f = new Fetcher(Source.Web, $@"https://sunspot.sdsu.edu/schedule/search?mode=search&period={code}&abbrev={_majorAbbreviation}");
                    Parser p = new Parser(f.Document, Parser.SelectByClassPattern("sectionMeeting"));


                    Course c_tmp = null;
                    int sec_tmp = -1;
                    int sch_tmp = -1;
                    float u_tmp = 0f;
                    foreach(HtmlNode node in p.Nodes)
                    {
                        SdsuMeetingBuilder b = new SdsuMeetingBuilder(code, node);

                        Period period = b.BuildPeriod();
                        Course course = b.BuildCourse() ?? c_tmp;
                        int section = b.BuildSection();
                        int schedule = b.BuildSchedule();
                        float units = b.BuildUnits();
                        Format format = b.BuildFormat();
                        Time time = b.BuildTime();
                        Day days = b.BuildDay();
                        Location location = b.BuildLocation();
                        string instructor = b.BuildInstructor();

                        Meeting m = new Meeting(
                            period,
                            course,
                            section == -1 ? sec_tmp : section,
                            schedule == -1 ? sch_tmp : schedule,
                            units < -1 ? u_tmp : units,
                            format,
                            time,
                            days,
                            location,
                            instructor
                            );
                        _meetings.Add(m);

                        c_tmp = course;
                        sec_tmp = section;
                        sch_tmp = schedule;
                        u_tmp = units;
                    }
                }
            }
            return _meetings;
        }
    }
}
