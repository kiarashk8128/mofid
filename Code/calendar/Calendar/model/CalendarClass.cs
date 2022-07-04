using System.Collections.Generic;

namespace Calendar.model
{
    public class CalendarClass
    {
        private string title;
        private string ownerName;
        private int id;
        private bool isEnabled;
        private List<Event> events = new List<Event>();
        private List<Task> tasks = new List<Task>();
        
        private static int staticID = 1;
        private static CalendarClass opendedCalendar;
        private static List<CalendarClass> calendars = new List<CalendarClass>();
        private static HashSet<CalendarClass> enabledCalendars = new HashSet<CalendarClass>();

        public CalendarClass(string ownerName, string title)
        {
            this.ownerName = ownerName;
            this.title = title;
            this.id = staticID++;
            User.GetUserByName(this.ownerName).AddCalendar(this);
            calendars.Add(this);
        }

        public string Title
        {
            get => title;
            set => title = value;
        }

        public int Id
        {
            get => id;
            set => id = value;
        }

        public string OwnerName
        {
            get => ownerName;
            set => ownerName = value;
        }

        public bool IsEnabled
        {
            get => isEnabled;
            set => isEnabled = value;
        }

        public static List<CalendarClass> Calendars
        {
            get => calendars;
            set => calendars = value;
        }

        public static HashSet<CalendarClass> EnabledCalendars
        {
            get => enabledCalendars;
            set => enabledCalendars = value;
        }

        public List<Event> Events => events;

        public List<Task> Tasks => tasks;

        public static CalendarClass OpendedCalendar
        {
            get => opendedCalendar;
            set => opendedCalendar = value;
        }

        public static CalendarClass GetCalendarById(int id)
        {
            foreach (var calendar in calendars)
            {
                if (calendar.id == id) return calendar;
            }

            return null;
        }
        
        public void AddEvent(Event aEvent)
        {
            this.events.Add(aEvent);
        }
        
        public void AddTask(Task task)
        {
            this.tasks.Add(task);
        }


    }
}