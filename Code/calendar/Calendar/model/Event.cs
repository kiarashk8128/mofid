using System;
using System.Collections.Generic;
using System.Globalization;

namespace Calendar.model
{
    public class Event : CalendarStuff
    {
        List<DateTime> dates = new List<DateTime>();

        
        private static List<Event> events = new List<Event>();
        

        public Event(int calendarId, string title, bool hasMeeting, string startDate, string endDate, char repeatMode)
        {
            this.CalendarClass = CalendarClass.GetCalendarById(calendarId);
            this.title = title;
            this.hasMeeting = hasMeeting;
            this.startDate = DateTime.ParseExact(startDate, "dd-MM-yyyy", CultureInfo.InvariantCulture);
            this.endDate = DateTime.ParseExact(endDate, "dd-MM-yyyy", CultureInfo.InvariantCulture);
            this.repeatMode = repeatMode;
            this.CalendarClass.AddEvent(this);
            events.Add(this);
        }
        
        
        public Event(int calendarId, string title, bool hasMeeting, string startDate, int repeatNo, char repeatMode)
        {
            this.CalendarClass = CalendarClass.GetCalendarById(calendarId);
            this.title = title;
            this.hasMeeting = hasMeeting;
            this.StartDate = DateTime.ParseExact(startDate, "dd-MM-yyyy", CultureInfo.InvariantCulture);
            this.repeatNo = repeatNo;
            this.repeatMode = repeatMode;
            this.CalendarClass.AddEvent(this);
            events.Add(this);
        }
        
        
        public Event(int calendarId, string title, bool hasMeeting, string startDate)
        {
            this.CalendarClass = CalendarClass.GetCalendarById(calendarId);
            this.title = title;
            this.hasMeeting = hasMeeting;
            this.startDate = DateTime.Parse(startDate);
            this.CalendarClass.AddEvent(this);
            events.Add(this);
        }

        public static Event GetEventByTitle(string title)
        {
            foreach (var aEvent in events)
            {
                if (aEvent.title == title)
                {
                    return aEvent;
                }
            }

            return null;
        }
        
        
        public List<DateTime> Dates
        {
            get => dates;
            set => dates = value;
        }

        public static List<Event> Events
        {
            get => events;
            set => events = value;
        }
    }
}