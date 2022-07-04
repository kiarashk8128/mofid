using System;
using System.Collections.Generic;
using System.Globalization;

namespace Calendar.model
{
    public class Task : CalendarStuff
    {
        private DateTime startTime;
        private DateTime endTime;
        
        List<DateTime> dates = new List<DateTime>();

        
        private static List<Task> tasks = new List<Task>();

        public Task(int calendarId, string title, bool hasMeeting, string startTime, string startDate, string endTime, string endDate, char repeatMode)
        {
            this.CalendarClass = CalendarClass.GetCalendarById(calendarId);
            this.title = title;
            this.hasMeeting = hasMeeting;
            this.startTime = DateTime.Parse(startTime);
            this.startDate = DateTime.ParseExact(startDate, "dd-MM-yyyy", CultureInfo.InvariantCulture);
            this.endTime = DateTime.Parse(endTime);
            this.endDate = DateTime.ParseExact(endDate, "dd-MM-yyyy", CultureInfo.InvariantCulture);
            this.repeatMode = repeatMode;
            this.CalendarClass.AddTask(this);
            tasks.Add(this);
        }
        
        public Task(int calendarId, string title, bool hasMeeting, string startTime, string startDate, string endTime, int repeatNo, char repeatMode)
        {
            this.CalendarClass = CalendarClass.GetCalendarById(calendarId);
            this.title = title;
            this.hasMeeting = hasMeeting;
            this.startTime = DateTime.Parse(startTime);
            this.startDate = DateTime.ParseExact(startDate, "dd-MM-yyyy", CultureInfo.InvariantCulture);
            this.endTime = DateTime.Parse(endTime);
            this.repeatNo = repeatNo;
            this.repeatMode = repeatMode;
            this.CalendarClass.AddTask(this);
            tasks.Add(this);
        }
        
        public Task(int calendarId, string title, bool hasMeeting, string startTime, string startDate, string endTime)
        {
            this.CalendarClass = CalendarClass.GetCalendarById(calendarId);
            this.title = title;
            this.hasMeeting = hasMeeting;
            this.startTime = DateTime.Parse(startTime);
            this.startDate = DateTime.ParseExact(startDate, "dd-MM-yyyy", CultureInfo.InvariantCulture);
            this.endTime = DateTime.Parse(endTime);
            this.CalendarClass.AddTask(this);
            tasks.Add(this);
        }

        public DateTime StartTime
        {
            get => startTime;
            set => startTime = value;
        }
        public DateTime EndTime
        {
            get => endTime;
            set => endTime = value;
        }
        
        public static Task GetTaskByTitle(string title)
        {
            foreach (var aTask in tasks)
            {
                if (aTask.title == title)
                {
                    return aTask;
                }
            }

            return null;
        }
        
        public List<DateTime> Dates
        {
            get => dates;
            set => dates = value;
        }

        public static List<Task> Tasks
        {
            get => tasks;
            set => tasks = value;
        }
    }
}