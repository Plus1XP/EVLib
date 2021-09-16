using System;
using System.Collections.Generic;
using System.Text;

namespace EVLlib.FileIO
{
    public class CalendarManager : FileManager
    {
        private StringBuilder calendarEntry;

        public CalendarManager()
        {

        }

        /// <summary>
        /// Converts a String to a UTC DateTime equivalent.
        /// </summary>
        /// <param name="dateTime">String representation of a date and time.</param>
        /// <returns>UTC DateTime.</returns>
        public DateTime ParseDateTimeToUTC(string dateTime)
        {
            DateTime.TryParse(dateTime, out DateTime parsedDateTime);
            return parsedDateTime.ToUniversalTime();
        }

        /// <summary>
        /// converts a String to a Local DateTime equivalent.
        /// </summary>
        /// <param name="dateTime">String representation of a date and time.</param>
        /// <returns>Local DateTime.</returns>
        public DateTime ParseDateTimeToLocal(string dateTime)
        {
            DateTime.TryParse(dateTime, out DateTime parsedDateTime);
            return parsedDateTime.ToLocalTime();
        }

        /// <summary>
        /// Creates an iCalendar entry header.
        /// (Note: Product ID must be in the format - CompanyName//Product//EN)
        /// </summary>
        /// <param name="productID">Specifies the identifier for the product that created the iCalendar object.</param>
        public void CreateCalendarEntry(string productID)
        {
            this.calendarEntry = new StringBuilder();
            this.calendarEntry.AppendLine("BEGIN:VCALENDAR");
            this.calendarEntry.AppendLine("VERSION:2.0");
            this.calendarEntry.AppendLine($"PRODID:-//{productID}");
        }

        /// <summary>
        /// Adds GMT timezone to the iCalendar entry.
        /// </summary>
        public void CreateGMTCalendarTimeZoneEntry()
        {
            this.calendarEntry.AppendLine("BEGIN:VTIMEZONE");
            this.calendarEntry.AppendLine("TZID:GMT Standard Time");
            this.calendarEntry.AppendLine("BEGIN:STANDARD");
            this.calendarEntry.AppendLine("DTSTART:16011028T020000");
            this.calendarEntry.AppendLine("RRULE:FREQ=YEARLY;BYDAY=-1SU;BYMONTH=10");
            this.calendarEntry.AppendLine("TZOFFSETTO:+0100");
            this.calendarEntry.AppendLine("TZOFFSETFROM:-0000");
            this.calendarEntry.AppendLine("END:STANDARD");
            this.calendarEntry.AppendLine("BEGIN:DAYLIGHT");
            this.calendarEntry.AppendLine("DTSTART:16010325T010000");
            this.calendarEntry.AppendLine("RRULE:FREQ=YEARLY;BYDAY=-1SU;BYMONTH=3");
            this.calendarEntry.AppendLine("TZOFFSETTO:-0000");
            this.calendarEntry.AppendLine("TZOFFSETFROM:+0100");
            this.calendarEntry.AppendLine("END:DAYLIGHT");
            this.calendarEntry.AppendLine("END:VTIMEZONE");
        }
    }
}
