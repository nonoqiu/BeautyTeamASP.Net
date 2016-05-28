using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BeautyTeamWeb
{
    public static class TimeService
    {
        public static bool OutOfRange(this TimeSpan? source)
        {
            return source > new TimeSpan(hours:23,minutes:59,seconds:59);
        }
    }
}