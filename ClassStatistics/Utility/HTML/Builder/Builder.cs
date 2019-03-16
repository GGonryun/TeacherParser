using Class;
using System;
using System.Collections.Generic;
using System.Text;

namespace Utility.HTML
{
    public abstract class Builder
    {
        public abstract Period BuildPeriod();
        public abstract Course BuildCourse();
        public abstract int BuildSection();
        public abstract int BuildSchedule();
        public abstract float BuildUnits();
        public abstract Format BuildFormat();
        public abstract Time BuildTime();
        public abstract Day BuildDay();
        public abstract Location BuildLocation();
        public abstract string BuildInstructor();
        public abstract Meeting GetResult();

    }
}
