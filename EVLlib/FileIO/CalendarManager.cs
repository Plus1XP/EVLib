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
        /// Starts an iCaldendar entry.
        /// </summary>
        /// <remarks>
        /// (Note: Product ID must be in the format - CompanyName//Product//EN)
        /// </remarks>
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

        /// <summary>
        /// Starts an iCalendar event.
        /// </summary>
        /// <param name="startTime">Event UTC DateTime Start.</param>
        /// <param name="endTime">Event UTC DateTime End.</param>
        /// <param name="subject">Event Subject.</param>
        /// <param name="location">Event location.</param>
        /// <param name="description">Event Description.</param>
        public void CreateCalendarEvent(DateTime startTime, DateTime endTime, string subject, string location, string description)
        {
            this.calendarEntry.AppendLine("BEGIN:VEVENT");

            // Specify the date time with the time zone stamp. 
            /*
            this.calendarEntry.AppendLine("DTSTART;TZID=GMT Standard Time:" + startTime.ToString("yyyyMMddTHHmm00"));
            this.calendarEntry.AppendLine("DTEND;TZID=GMT Standard Time:" + endTime.ToString("yyyyMMddTHHmm00"));           
            */

            // Specify the date time in UTC (Z).
            this.calendarEntry.AppendLine("DTSTART:" + startTime.ToString("yyyyMMddTHHmm00Z"));
            this.calendarEntry.AppendLine("DTEND:" + endTime.ToString("yyyyMMddTHHmm00Z"));

            this.calendarEntry.AppendLine("SUMMARY:" + subject);
            this.calendarEntry.AppendLine("LOCATION:" + location);
            this.calendarEntry.AppendLine("DESCRIPTION:" + description);
        }

        /// <summary>
        /// Adds alarm to the iCalendar entry.
        /// </summary>
        /// <remarks>
        /// -PT will alaram before the trigger,
        /// PT will alarma after the trigger.
        /// </remarks>
        /// <param name="trigger">Specifies when the alarm will trigger (minutes).</param>
        /// <param name="subject">Alarm Subject.</param>
        public void CreateCalendarAlarmEntry(int trigger, string subject)
        {
            this.calendarEntry.AppendLine("BEGIN: VALARM");

            // Sets a duration or time to trigger the arlarm (Mins or Hours).
            this.calendarEntry.AppendLine("TRIGGER:-PT" + trigger + "M");
            this.calendarEntry.AppendLine("ACTION:DISPLAY");
            this.calendarEntry.AppendLine("DESCRIPTION:" + subject);
            this.calendarEntry.AppendLine("END:VALARM");
        }

        /// <summary>
        /// Closes the iCalendar event.
        /// </summary>
        public void CloseEventEntry()
        {
            this.calendarEntry.AppendLine("END:VEVENT");
        }

        /// <summary>
        /// Closes the iCalendar entry.
        /// </summary>
        public void CloseCalendarEntry()
        {
            this.calendarEntry.AppendLine("END:VCALENDAR");
        }

        /// <summary>
        /// Retrives all iCalendar entries.
        /// </summary>
        /// <returns>iCalendar entries as a String.</returns>
        private string RetrieveCalendarEntries()
        {
            return this.calendarEntry.ToString();
        }

        /// <summary>
        /// If ICS file exists, clears file. Otherwise creates a new file.
        /// </summary>
        /// <param name="filePath">Path to file.</param>
        private void ValidateFileIsUsable(string filePath)
        {
            if (this.IsFileCreated(filePath))
            {
                this.ClearFile(filePath);
            }
            else
            {
                this.CreateFile(filePath);
            }
        }

        /// <TODO>
        /// Validate if file was save sucessfully or not.
        /// </TODO>
        /// 
        /// <summary>
        /// Writes all iCalendar entries to an ICS file.
        /// </summary>
        /// <param name="filePath">Path to file.</param>
        /// <returns>Validaton string if succesfull.</returns>
        public string CreateICSFile(string filePath)
        {
            this.ValidateFileIsUsable(filePath);
            this.SaveToFile(filePath, this.RetrieveCalendarEntries());
            return "ICS file created";
        }

        /// <TODO>
        /// Validate if file was read sucessfully or not.
        /// </TODO>
        /// 
        /// <summary>
        /// Checks if ICS file exists then reads it.
        /// </summary>
        /// <param name="filePath">Path to file.</param>
        /// <returns>All lines from ICS file, or "No Data" depending on if file exists.</returns>
        public string ReadICSFile(string filePath)
        {
            return this.IsFileCreated(filePath) ? this.ReadStringFromFile(filePath) : "No Data";
        }

        /// <TODO>
        /// Validate if file was deleted sucessfully or not.
        /// </TODO>
        /// 
        /// <summary>
        /// Checks if ICS file exists then deletes it.
        /// </summary>
        /// <param name="filePath">Path to file.</param>
        /// <returns>Confirmation on if the ICS file was deleted.</returns>
        public string DeleteICSFile(string filePath)
        {
            if (this.IsFileCreated(filePath))
            {
                this.DeleteFile(filePath);
                return $"{filePath} Deleted";
            }
            else
            {
                return "No File";
            }
        }
    }
}
