using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using Calendar.model;
using Calendar.view;

namespace Calendar.controller
{
    public class MainController
    {
        private static MainController instance;

        private MainController()
        {
        }

        public static MainController GetInstance()
        {
            if (instance == null)
                instance = new MainController();
            return instance;
        }


        public string Create(string ownerName, string title)
        {
            if (Regex.IsMatch(title, "^[0-9A-Za-z_]*$"))
            {
                CalendarClass calendar = new CalendarClass(ownerName, title);
                return Responses.SuccessfulCalendarCreating;
            }

            return Responses.InvalidTitle;
        }

        public string Open(int id)
        {
            if (CalendarClass.GetCalendarById(id) != null)
            {
                CalendarClass calendar = CalendarClass.GetCalendarById(id);
                
                
                
                if (User.LoggedInUser.Calendars.Contains(calendar))
                {
                    CalendarClass.OpendedCalendar = calendar;
                    Menu.currentMenu = MenuName.CalendarMenu;
                    return Responses.SuccessfulCalendarOpening + "\n" + Responses.CalendarMenu;
                }

                return Responses.YouHaveNotThisCalendar;
            }

            return Responses.NotExistedCalendar;
        }

        public string Enable(int id)
        {
            if (CalendarClass.GetCalendarById(id) != null)
            {
                CalendarClass calendar = CalendarClass.GetCalendarById(id);
                if (User.LoggedInUser.Calendars.Contains(calendar))
                {
                    CalendarClass.EnabledCalendars.Add(calendar);
                    calendar.IsEnabled = true;
                    return Responses.SuccessfulCalendarEnabling;
                }

                return Responses.YouHaveNotThisCalendar;
            }

            return Responses.NotExistedCalendar;
        }

        public string Disable(int id)
        {
            if (CalendarClass.GetCalendarById(id) != null)
            {
                CalendarClass calendar = CalendarClass.GetCalendarById(id);
                if (User.LoggedInUser.Calendars.Contains(calendar))
                {
                    if (calendar.IsEnabled)
                    {
                        CalendarClass.EnabledCalendars.Remove(calendar);
                        calendar.IsEnabled = false;
                    }

                    return Responses.SuccessfulCalendarDisabling;
                }

                return Responses.YouHaveNotThisCalendar;
            }

            return Responses.NotExistedCalendar;
        }

        public string Delete(int id)
        {
            if (CalendarClass.GetCalendarById(id) != null)
            {
                CalendarClass calendar = CalendarClass.GetCalendarById(id);
                if (User.LoggedInUser.Calendars.Contains(calendar))
                {
                    if (calendar.OwnerName == User.LoggedInUser.Name)
                    {
                        if (CalendarClass.EnabledCalendars.Contains(calendar))
                        {
                            CalendarClass.EnabledCalendars.Remove(calendar);
                        }

                        foreach (User aUser in User.Users)
                        {
                            if (aUser.Calendars.Contains(calendar))
                            {
                                aUser.Calendars.Remove(calendar);
                            }
                        }

                        CalendarClass.Calendars.Remove(calendar);
                        return Responses.SuccessfulCalendarDeleting;
                    }

                    return Responses.YouHaveNotAccessToDeleteThisCalendar;
                }

                return Responses.YouHaveNotThisCalendar;
            }

            return Responses.NotExistedCalendar;
        }


        public string Remove(int id)
        {
            if (CalendarClass.GetCalendarById(id) != null)
            {
                CalendarClass calendar = CalendarClass.GetCalendarById(id);
                if (User.LoggedInUser.Calendars.Contains(calendar))
                {
                    if (calendar.OwnerName != User.LoggedInUser.Name)
                    {
                        User.LoggedInUser.Calendars.Remove(calendar);
                        return Responses.SuccessfulCalendarRemoving;
                    }
                    else
                    {
                        Console.WriteLine(Responses.AskToRemoving);
                        string answer = Console.ReadLine()?.ToLower();
                        if (answer == "yes")
                        {
                            return Delete(id);
                        }
                        if (answer == "no")
                        {
                            return Responses.NO;
                        }
                    }
                }

                return Responses.YouHaveNotThisCalendar;
            }

            return Responses.NotExistedCalendar;
        }

        public string Edit(int id, string newTitle)
        {
            if (CalendarClass.GetCalendarById(id) != null)
            {
                CalendarClass calendar = CalendarClass.GetCalendarById(id);
                if (User.LoggedInUser.Calendars.Contains(calendar))
                {
                    if (User.LoggedInUser.Name == calendar.OwnerName)
                    {
                        if (Regex.IsMatch(newTitle, "^[0-9A-Za-z_]*$"))
                        {
                            calendar.Title = newTitle;
                            return Responses.SuccessfulTitleEditing;
                        }

                        return Responses.InvalidTitle;
                    }

                    return Responses.YouHaveNotAccessToEditThisCalendar;
                }

                return Responses.YouHaveNotThisCalendar;
            }

            return Responses.YouHaveNotThisCalendar;
        }

        public string Share(int id, string listOfUsers)
        {
            if (CalendarClass.GetCalendarById(id) != null)
            {
                CalendarClass calendar = CalendarClass.GetCalendarById(id);
                if (User.LoggedInUser.Calendars.Contains(calendar))
                {
                    if (User.LoggedInUser.Name == calendar.OwnerName)
                    {
                        List<string> list = new List<string>(Regex.Split(listOfUsers, "\\s+"));
                        foreach (string name in list)
                        {
                            if (Regex.IsMatch(name, "^[0-9A-Za-z_]*$"))
                            {
                                if (User.GetUserByName(name) != null)
                                {
                                    continue;
                                }

                                return Responses.NotExistedUser;
                            }

                            return Responses.InvalidUserName;
                        }

                        foreach (string name in list)
                        {
                            User.GetUserByName(name).Calendars.Add(calendar);
                        }

                        return Responses.SuccessfulCalendarSharing;
                    }

                    return Responses.YouHaveNotAccessToShareThisCalendar;
                }

                return Responses.YouHaveNotThisCalendar;
            }

            return Responses.NotExistedCalendar;
        }


        public string ShowDate(string inputDate)
        {
            if (Tools.IsDateValid(inputDate))
            {
                DateTime date = DateTime.ParseExact(inputDate, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                StringBuilder events = new StringBuilder();
                StringBuilder tasks = new StringBuilder();
                foreach (var calendar in CalendarClass.EnabledCalendars)
                {
                    events.Append($"events on {date}:");
                    calendar.Events.Sort((x, y) => String.Compare(x.Title, y.Title, StringComparison.Ordinal));
                    foreach (var aEvent in calendar.Events)
                    {
                        if (aEvent.Dates.Contains(date))
                        {
                            var hasMeeting = aEvent.HasMeeting ? "T" : "F";
                            events.Append($"\ntitle: {aEvent.Title} | has meeting: {hasMeeting}");
                        }
                    }

                    if (events.ToString().Length == 32)
                    {
                        events.Clear();
                        events.Append($"events on {date}:\n");
                        events.Append(Responses.Nothing);
                    }

                    tasks.Append($"\ntasks on {date}:");
                    calendar.Tasks.Sort((x, y) => DateTime.Compare(x.StartTime, y.StartTime));
                    foreach (var aTask in calendar.Tasks)
                    {
                        if (aTask.Dates.Contains(date))
                        {
                            var hasMeeting = aTask.HasMeeting ? "T" : "F";
                            tasks.Append($"\ntitle: {aTask.Title} | has meeting: {hasMeeting} | start: {aTask.StartTime.Hour}:{aTask.StartTime.Minute}");
                        }
                    }

                    if (tasks.ToString().Length == 32)
                    {
                        tasks.Clear();
                        tasks.Append($"\ntasks on {date}:\n");
                        tasks.Append(Responses.Nothing);
                    }
                }

                StringBuilder result = new StringBuilder(events.ToString());
                result.Append(tasks);
                return result.ToString();
            }

            return Responses.InvalidDate;
        }


        public string ShowCalendars()
        {
            StringBuilder calendars = new StringBuilder("your calendar:");
            if (CalendarClass.Calendars.Count != 0)
            {
                foreach (var calendar in User.LoggedInUser.Calendars)
                {
                    calendars.Append($"\ntitle: {calendar.Title} | id: {calendar.Id} | owner: {calendar.OwnerName}");
                }

                if (calendars.ToString().Length == 0)
                {
                    calendars.Clear();
                    calendars.Append(Responses.Nothing);
                }

                return calendars.ToString();
            }

            return Responses.Nothing;
        }


        public string ShowEnabledCalendars()
        {
            StringBuilder enabledCalendars = new StringBuilder("enabled calendar:");
            if (CalendarClass.Calendars.Count != 0)
            {
                foreach (var calendar in User.LoggedInUser.Calendars)
                {
                    if (calendar.IsEnabled)
                    {
                            enabledCalendars.Append($"\ntitle: {calendar.Title} | id: {calendar.Id} | owner: {calendar.OwnerName}");    
                    }
                }

                if (enabledCalendars.ToString().Length == 0)
                {
                    enabledCalendars.Clear();
                    enabledCalendars.Append(Responses.Nothing);
                }

                return enabledCalendars.ToString();
            }

            return Responses.Nothing;
        }

        public string Logout()
        {
            User.LoggedInUser = default;
            Menu.currentMenu = MenuName.RegisterMenu;
            return Responses.SuccessfulLogout + "\n" + Responses.RegisterMenu;
        }
    }
}