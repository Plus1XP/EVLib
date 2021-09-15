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
    }
}
