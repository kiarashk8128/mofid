using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Calendar.controller
{
    public class Tools
    {
        public static Match PatternMatcher(string input, string pattern)
        {
            Match match = Regex.Match(input, pattern, RegexOptions.IgnoreCase);
            return match;
        }

        public static bool IsDateValid(string tempDate)
        {
            DateTime fromDateValue;
            var formats = new[] { "dd-MM-yyyy", "dd-MM-yyyy hh:mm", "hh:mm" };
            return DateTime.TryParseExact(tempDate, formats, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out fromDateValue);
        }

        public static bool IsEndDateGreaterThanStartDate(string endDate, string startDate)
        {
            var end = DateTime.ParseExact(endDate, "dd-MM-yyyy", CultureInfo.InvariantCulture);
            var start = DateTime.ParseExact(startDate, "dd-MM-yyyy", CultureInfo.InvariantCulture);
            return end >= start;
        }
        
        public static bool IsEndTimeGreaterThanStartTime(string endTime, string startDate)
        {
            var end = DateTime.Parse(endTime);
            var start = DateTime.Parse(startDate);
            return end >= start;
        }
    }
}