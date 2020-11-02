using System;
using System.Collections.Generic;
using System.Text;

namespace Foodie.Core
{
    public struct DateRange
    {
        public static DateRange ByYear(int year)
        {
            return new DateRange(new DateTime(year, 1, 1), new DateTime(year + 1, 1, 1).AddDays(-1));
        }

        public DateRange(DateTime start, DateTime end)
        {
            Start = start;
            End = end;
        }

        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }
}
