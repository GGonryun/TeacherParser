namespace ClassStatistics
{
    public class Time
    {
        public int Hour { get; private set; }
        public int Minute { get; private set; }
        public Period Period { get; private set; }

        public Time(int hour, int minute, Period period)
        {
            this.Hour = hour;
            this.Minute = minute;
            this.Period = period;
        }

        public static Time ToMilitary(Time time)
        {
            if(time.Period == Period.MIL)
            {
                throw new System.Exception("The requested time is already in Military time!");
            }
            else
            {
                int hour = time.Period == Period.AM ? (time.Hour == 12 ? 0 : time.Hour) : ( time.Hour == 12 ? time.Hour : time.Hour + 12);
                int minute = time.Minute;
                Period period = Period.MIL;
                return new Time(hour, minute, period);
            }
        }

        public static Time ToStandard(Time time)
        {

        }
    }
}
