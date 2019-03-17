using System;
using System.Collections.Generic;
using System.Text;

namespace Utility
{
    public static class EnumParser
    {
        public static T ParseEnum<T>(this string value)
        {
            return (T)Enum.Parse(typeof(T), value, true);
        }
    }
}
