using System;
using System.Text;
using System.Text.RegularExpressions;
using Calendar.model;
using Calendar.view;

namespace Calendar.controller
{
    public class CalendarController
    {
        private static CalendarController instance;

        private CalendarController()
        {
        }

        public static CalendarController GetInstance()
        {
            if (instance == null) instance = new CalendarController();
            return instance;
        }


        public string ShowEvents()
        {
            StringBuilder events = new StringBuilder();
            events.Append(Responses.ShowEvents);
            CalendarClass.OpendedCalendar.Events.Sort((x, y) =>
                String.Compare(x.Title, y.Title, StringComparison.Ordinal));
            foreach (var aEvent in CalendarClass.OpendedCalendar.Events)
            {
                var hasMeeting = aEvent.HasMeeting ? "T" : "F";
                string mode = default;
                if (aEvent.RepeatMode == 'D') mode = "Daily";
                else if (aEvent.RepeatMode == 'W') mode = "Weekly";
                else if (aEvent.RepeatMode == 'M') mode = "monthly";
                events.Append($"\ntitle: {aEvent.Title} | has meeting: {hasMeeting} | start: {aEvent.StartDate.Day}/{aEvent.StartDate.Month}/{aEvent.StartDate.Year} | mode: {mode}");
            }

            if (events.Length == 24)
            {
                events.Clear();
                events.Append(Responses.ShowEvents);
                events.Append("\n" + Responses.Nothing);
            }

            return events.ToString();
        }


        public string ShowTasks()
        {
            StringBuilder tasks = new StringBuilder();
            tasks.Append(Responses.ShowTasks);
            CalendarClass.OpendedCalendar.Tasks.Sort((x, y) => DateTime.Compare(x.StartDate, y.StartDate));
            foreach (var aTask in CalendarClass.OpendedCalendar.Tasks)
            {
                var hasMeeting = aTask.HasMeeting ? "T" : "F";
                string mode = default;
                if (aTask.RepeatMode == 'D') mode = "Daily";
                else if (aTask.RepeatMode == 'W') mode = "Weekly";
                else if (aTask.RepeatMode == 'M') mode = "monthly";
                tasks.Append($"\ntitle: {aTask.Title} | has meeting: {hasMeeting} | start: {aTask.StartDate.Day}/{aTask.StartDate.Month}/{aTask.StartDate.Year} {aTask.StartTime.Hour}:{aTask.StartTime.Minute} | mode: {mode}");
            }

            if (tasks.Length == 24)
            {
                tasks.Clear();
                tasks.Append(Responses.ShowEvents);
                tasks.Append("\n" + Responses.Nothing);
            }

            return tasks.ToString();
        }


        public string AddEvent(string title, string startDate, string field, char repeatMode, string hasMeetingStr)
        {
            bool hasMeeting = default;
            if (User.LoggedInUser.Name == CalendarClass.OpendedCalendar.OwnerName)
            {
                if (Regex.IsMatch(title, "^[0-9A-Za-z_]*$"))
                {
                    if (Tools.IsDateValid(startDate) && Regex.IsMatch(startDate, "^\\d\\d-\\d\\d-\\d\\d\\d\\d$"))
                    {
                        if (Regex.IsMatch(field, "^\\d\\d-\\d\\d-\\d\\d\\d\\d$"))
                        {
                            if (Tools.IsDateValid(field) && Tools.IsEndDateGreaterThanStartDate(field, startDate))
                            {
                                if (Event.GetEventByTitle(title) == null)
                                {
                                    if (hasMeetingStr == "T")
                                    {
                                        var link = Console.ReadLine();
                                        hasMeeting = true;
                                    }

                                    Event newEvent = new Event(CalendarClass.OpendedCalendar.Id, title, hasMeeting,
                                        startDate, field,
                                        repeatMode);
                                    DateTime temp = newEvent.StartDate;
                                    while (temp <= newEvent.EndDate)
                                    {
                                        newEvent.Dates.Add(temp);
                                        if (newEvent.RepeatMode == 'D')
                                        {
                                            temp = temp.AddDays(1);
                                        }
                                        else if (newEvent.RepeatMode == 'W')
                                        {
                                            temp = temp.AddDays(7);
                                        }
                                        else if (newEvent.RepeatMode == 'M')
                                        {
                                            temp = temp.AddMonths(1);
                                        }
                                    }

                                    return Responses.SuccessfulEventAdding;
                                }

                                return Responses.ExistedEvent;
                            }

                            return Responses.InvalidEndDate;
                        }

                        if (Regex.IsMatch(field, "^[0-9]+$"))
                        {
                            if (Event.GetEventByTitle(title) == null)
                            {
                                if (hasMeetingStr == "T")
                                {
                                    var link = Console.ReadLine();
                                    hasMeeting = true;
                                }

                                Event newEvent = new Event(CalendarClass.OpendedCalendar.Id, title, hasMeeting,
                                    startDate,
                                    Convert.ToInt32(field), repeatMode);
                                DateTime temp = newEvent.StartDate;
                                for (int i = 0; i < newEvent.RepeatNo; i++)
                                {
                                    newEvent.Dates.Add(temp);
                                    if (newEvent.RepeatMode == 'D')
                                    {
                                        temp = temp.AddDays(1);
                                    }
                                    else if (newEvent.RepeatMode == 'W')
                                    {
                                        temp = temp.AddDays(7);
                                    }
                                    else if (newEvent.RepeatMode == 'M')
                                    {
                                        temp = temp.AddMonths(1);
                                    }
                                }

                                return Responses.ExistedEvent;
                            }
                        }

                        if (Regex.IsMatch(field, "None"))
                        {
                            if (Event.GetEventByTitle(title) == null)
                            {
                                if (hasMeetingStr == "T")
                                {
                                    var link = Console.ReadLine();
                                    hasMeeting = true;
                                }

                                Event newEvent = new Event(CalendarClass.OpendedCalendar.Id, title, hasMeeting,
                                    startDate);
                                return Responses.SuccessfulEventAdding;
                            }

                            return Responses.ExistedEvent;
                        }

                        return Responses.InvalidCommand;
                    }

                    return Responses.InvalidStartDate;
                }

                return Responses.InvalidTitle;
            }

            return Responses.YouHaveNotAccessToAddOrEditOrDeleteEventsOrTasks;
        }


        public string AddTask(string title, string startTime, string endTime, string startDate, string field,
            char repeatMode, string hasMeetingStr)
        {
            bool hasMeeting = default;
            if (User.LoggedInUser.Name == CalendarClass.OpendedCalendar.OwnerName)
            {
                if (Regex.IsMatch(title, "^[0-9A-Za-z_]*$"))
                {
                    if (Tools.IsDateValid(startDate) && Regex.IsMatch(startDate, "^\\d\\d-\\d\\d-\\d\\d\\d\\d$"))
                    {
                        if (Regex.IsMatch(startTime, "^\\d\\d:\\d\\d$"))
                        {
                            if (Tools.IsEndTimeGreaterThanStartTime(endTime, startTime) && Regex.IsMatch(endTime, "^\\d\\d:\\d\\d$"))
                            {
                                if (Regex.IsMatch(field, "^\\d\\d-\\d\\d-\\d\\d\\d\\d$"))
                                {
                                    if (Tools.IsDateValid(field) &&
                                        Tools.IsEndDateGreaterThanStartDate(field, startDate))
                                    {
                                        if (Task.GetTaskByTitle(title) == null)
                                        {
                                            if (hasMeetingStr == "T")
                                            {
                                                var link = Console.ReadLine();
                                                hasMeeting = true;
                                            }

                                            Task newTask = new Task(CalendarClass.OpendedCalendar.Id, title, hasMeeting,
                                                startTime, startDate,
                                                endTime, field, repeatMode);
                                            DateTime temp = newTask.StartDate;
                                            while (temp <= newTask.EndDate)
                                            {
                                                newTask.Dates.Add(temp);
                                                if (newTask.RepeatMode == 'D')
                                                {
                                                    temp = temp.AddDays(1);
                                                }
                                                else if (newTask.RepeatMode == 'W')
                                                {
                                                    temp = temp.AddDays(7);
                                                }
                                                else if (newTask.RepeatMode == 'M')
                                                {
                                                    temp = temp.AddMonths(1);
                                                }
                                            }

                                            return Responses.SuccessfulTaskAdding;
                                        }

                                        return Responses.ExistedTask;
                                    }

                                    return Responses.InvalidEndDate;
                                }

                                else if (Regex.IsMatch(field, "^[0-9]+$"))
                                {
                                    if (Task.GetTaskByTitle(title) == null)
                                    {
                                        if (hasMeetingStr == "T")
                                        {
                                            var link = Console.ReadLine();
                                            hasMeeting = true;
                                        }

                                        Task newTask = new Task(CalendarClass.OpendedCalendar.Id, title, hasMeeting,
                                            startTime, startDate, endTime,
                                            Convert.ToInt32(field), repeatMode);
                                        DateTime temp = newTask.StartDate;
                                        for (int i = 0; i < newTask.RepeatNo; i++)
                                        {
                                            newTask.Dates.Add(temp);
                                            if (newTask.RepeatMode == 'D')
                                            {
                                                temp = temp.AddDays(1);
                                            }
                                            else if (newTask.RepeatMode == 'W')
                                            {
                                                temp = temp.AddDays(7);
                                            }
                                            else if (newTask.RepeatMode == 'M')
                                            {
                                                temp = temp.AddMonths(1);
                                            }
                                        }

                                        return Responses.SuccessfulTaskAdding;
                                    }

                                    return Responses.ExistedTask;
                                }

                                else if (Regex.IsMatch(field, "None"))
                                {
                                    if (Task.GetTaskByTitle(title) == null)
                                    {
                                        if (hasMeetingStr == "T")
                                        {
                                            var link = Console.ReadLine();
                                            hasMeeting = true;
                                        }

                                        Task newTask = new Task(CalendarClass.OpendedCalendar.Id, title, hasMeeting,
                                            startTime, startDate, endTime);
                                        return Responses.SuccessfulTaskAdding;
                                    }

                                    return Responses.ExistedTask;
                                }

                                return Responses.InvalidCommand;
                            }

                            return Responses.InvalidEndTime;
                        }

                        return Responses.InvalidStartTime;
                    }

                    return Responses.InvalidStartDate;
                }

                return Responses.InvalidTitle;
            }

            return Responses.YouHaveNotAccessToAddOrEditOrDeleteEventsOrTasks;
        }


        public string EditEvent(string title, string field, string newValue)
        {
            if (CalendarClass.OpendedCalendar.Events.Contains(Event.GetEventByTitle(title)))
            {
                Event @event = Event.GetEventByTitle(title);
                if (User.LoggedInUser.Name == CalendarClass.OpendedCalendar.OwnerName)
                {
                    if (Regex.IsMatch(title, "^[0-9A-Za-z_]*$"))
                    {
                        switch (field)
                        {
                            case "meet":
                                if (newValue == "T")
                                {
                                    var link = Console.ReadLine();
                                    @event.HasMeeting = true;
                                }

                                if (newValue == "F")
                                {
                                    @event.HasMeeting = false;
                                }

                                return Responses.SuccessfulEventEditing;


                            case "KindOfRepeat":
                                @event.Dates.Clear();
                                if (newValue == "D")
                                {
                                    @event.RepeatMode = Convert.ToChar(newValue);
                                    if (@event.EndDate != null)
                                    {
                                        DateTime temp = @event.StartDate;
                                        while (temp <= @event.EndDate)
                                        {
                                            @event.Dates.Add(temp);
                                            temp = temp.AddDays(1);
                                        }
                                    } else if (@event.RepeatNo != null)
                                    {
                                        DateTime temp = @event.StartDate;
                                        for (int i = 0; i < @event.RepeatNo; i++)
                                        {
                                            @event.Dates.Add(temp);
                                            temp = temp.AddDays(1);
                                        }
                                    }
                                }

                                if (newValue == "W")
                                {
                                    @event.RepeatMode = Convert.ToChar(newValue);
                                    if (@event.EndDate != null)
                                    {
                                        DateTime temp = @event.StartDate;
                                        while (temp <= @event.EndDate)
                                        {
                                            @event.Dates.Add(temp);
                                            temp = temp.AddDays(7);
                                        }
                                    } else if (@event.RepeatNo != null)
                                    {
                                        DateTime temp = @event.StartDate;
                                        for (int i = 0; i < @event.RepeatNo; i++)
                                        {
                                            @event.Dates.Add(temp);
                                            temp = temp.AddDays(7);
                                        }
                                    }
                                }

                                if (newValue == "M")
                                {
                                    @event.RepeatMode = Convert.ToChar(newValue);
                                    if (@event.EndDate != null)
                                    {
                                        DateTime temp = @event.StartDate;
                                        while (temp <= @event.EndDate)
                                        {
                                            @event.Dates.Add(temp);
                                            temp = temp.AddMonths(1);
                                        }
                                    } else if (@event.RepeatNo != null)
                                    {
                                        DateTime temp = @event.StartDate;
                                        for (int i = 0; i < @event.RepeatNo; i++)
                                        {
                                            @event.Dates.Add(temp);
                                            temp = temp.AddMonths(1);
                                        }
                                    }
                                }

                                return Responses.SuccessfulEventEditing;

                            case "repeat":
                                if (Regex.IsMatch(newValue, "^\\d\\d-\\d\\d-\\d\\d\\d\\d$") &&
                                    Tools.IsEndDateGreaterThanStartDate(field, @event.StartDate.ToString()))
                                {
                                    @event.EndDate = DateTime.Parse(newValue);
                                    @event.RepeatNo = null;
                                    DateTime temp = @event.StartDate;
                                    @event.Dates.Clear();
                                    while (temp <= @event.EndDate)
                                    {
                                        @event.Dates.Add(temp);
                                        if (@event.RepeatMode == 'D')
                                        {
                                            temp = temp.AddDays(1);
                                        }
                                        else if (@event.RepeatMode == 'W')
                                        {
                                            temp = temp.AddDays(7);
                                        }
                                        else if (@event.RepeatMode == 'M')
                                        {
                                            temp = temp.AddMonths(1);
                                        }
                                    }
                                }
                                else if (Regex.IsMatch(newValue, "^[0-9]+$"))
                                {
                                    @event.RepeatNo = int.Parse(newValue);
                                    @event.EndDate = null;
                                    DateTime temp = @event.StartDate;
                                    @event.Dates.Clear();
                                    for (int i = 0; i < @event.RepeatNo; i++)
                                    {
                                        @event.Dates.Add(temp);
                                        if (@event.RepeatMode == 'D')
                                        {
                                            temp = temp.AddDays(1);
                                        }
                                        else if (@event.RepeatMode == 'W')
                                        {
                                            temp = temp.AddDays(7);
                                        }
                                        else if (@event.RepeatMode == 'M')
                                        {
                                            temp = temp.AddMonths(1);
                                        }
                                    }
                                }
                                else if (Regex.IsMatch(newValue, "None"))
                                {
                                    @event.Dates.Clear();
                                    @event.EndDate = null;
                                    @event.RepeatNo = null;
                                }

                                return Responses.SuccessfulEventEditing;

                            case "title":
                                if (User.LoggedInUser.Name == CalendarClass.OpendedCalendar.OwnerName)
                                {
                                    if (Regex.IsMatch(title, "^[0-9A-Za-z_]*$"))
                                    {
                                        if (Regex.IsMatch(newValue, "^[0-9A-Za-z_]*$"))
                                        {
                                            @event.Title = newValue;
                                            return Responses.SuccessfulEventEditing;
                                        }

                                        return Responses.InvalidNewTitle;
                                    }   

                                    return Responses.InvalidTitle;
                                }

                                return Responses.YouHaveNotAccessToAddOrEditOrDeleteEventsOrTasks;
                        }
                    }

                    return Responses.InvalidTitle;
                }

                return Responses.YouHaveNotAccessToAddOrEditOrDeleteEventsOrTasks;
            }

            return Responses.NotExistedEvent;
        }


        public string EditTask(string title, string field, string newValue)
        {
            if (CalendarClass.OpendedCalendar.Tasks.Contains(Task.GetTaskByTitle(title)))
            {
                Task task = Task.GetTaskByTitle(title);
                if (User.LoggedInUser.Name == CalendarClass.OpendedCalendar.OwnerName)
                {
                    if (Regex.IsMatch(title, "^[0-9A-Za-z_]*$"))
                    {
                        switch (field)
                        {
                            case "meet":
                                if (newValue == "T")
                                {
                                    var link = Console.ReadLine();
                                    task.HasMeeting = true;
                                }

                                if (newValue == "F")
                                {
                                    task.HasMeeting = false;
                                }

                                return Responses.SuccessfulTaskEditing;


                            case "KindOfRepeat":
                                task.Dates.Clear();
                                if (newValue == "D")
                                {
                                    task.RepeatMode = Convert.ToChar(newValue);
                                    if (task.EndDate != null)
                                    {
                                        DateTime temp = task.StartDate;
                                        while (temp <= task.EndDate)
                                        {
                                            task.Dates.Add(temp);
                                            temp = temp.AddDays(1);
                                        }
                                    } else if (task.RepeatNo != null)
                                    {
                                        DateTime temp = task.StartDate;
                                        for (int i = 0; i < task.RepeatNo; i++)
                                        {
                                            task.Dates.Add(temp);
                                            temp = temp.AddDays(1);
                                        }
                                    }
                                }

                                if (newValue == "W")
                                {
                                    task.RepeatMode = Convert.ToChar(newValue);
                                    if (task.EndDate != null)
                                    {
                                        DateTime temp = task.StartDate;
                                        while (temp <= task.EndDate)
                                        {
                                            task.Dates.Add(temp);
                                            temp = temp.AddDays(7);
                                        }
                                    } else if (task.RepeatNo != null)
                                    {
                                        DateTime temp = task.StartDate;
                                        for (int i = 0; i < task.RepeatNo; i++)
                                        {
                                            task.Dates.Add(temp);
                                            temp = temp.AddDays(7);
                                        }
                                    }
                                }

                                if (newValue == "M")
                                {
                                    task.RepeatMode = Convert.ToChar(newValue);
                                    if (task.EndDate != null)
                                    {
                                        DateTime temp = task.StartDate;
                                        while (temp <= task.EndDate)
                                        {
                                            task.Dates.Add(temp);
                                            temp = temp.AddMonths(1);
                                        }
                                    } else if (task.RepeatNo != null)
                                    {
                                        DateTime temp = task.StartDate;
                                        for (int i = 0; i < task.RepeatNo; i++)
                                        {
                                            task.Dates.Add(temp);
                                            temp = temp.AddMonths(1);
                                        }
                                    }
                                }

                                return Responses.SuccessfulEventEditing;

                            case "repeat":
                                if (Regex.IsMatch(newValue, "^\\d\\d-\\d\\d-\\d\\d\\d\\d$") &&
                                    Tools.IsEndDateGreaterThanStartDate(field, task.StartDate.ToString()))
                                {
                                    task.EndDate = DateTime.Parse(newValue);
                                    task.RepeatNo = null;
                                    DateTime temp = task.StartDate;
                                    task.Dates.Clear();
                                    while (temp <= task.EndDate)
                                    {
                                        task.Dates.Add(temp);
                                        if (task.RepeatMode == 'D')
                                        {
                                            temp = temp.AddDays(1);
                                        }
                                        else if (task.RepeatMode == 'W')
                                        {
                                            temp = temp.AddDays(7);
                                        }
                                        else if (task.RepeatMode == 'M')
                                        {
                                            temp = temp.AddMonths(1);
                                        }
                                    }
                                }
                                else if (Regex.IsMatch(newValue, "^[0-9]+$"))
                                {
                                    task.RepeatNo = int.Parse(newValue);
                                    task.EndDate = null;
                                    DateTime temp = task.StartDate;
                                    task.Dates.Clear();
                                    for (int i = 0; i < task.RepeatNo; i++)
                                    {
                                        task.Dates.Add(temp);
                                        if (task.RepeatMode == 'D')
                                        {
                                            temp = temp.AddDays(1);
                                        }
                                        else if (task.RepeatMode == 'W')
                                        {
                                            temp = temp.AddDays(7);
                                        }
                                        else if (task.RepeatMode == 'M')
                                        {
                                            temp = temp.AddMonths(1);
                                        }
                                    }
                                }
                                else if (Regex.IsMatch(newValue, "None"))
                                {
                                    task.Dates.Clear();
                                    task.EndDate = null;
                                    task.RepeatNo = null;
                                }

                                return Responses.SuccessfulTaskEditing;

                            case "title":
                                if (User.LoggedInUser.Name == CalendarClass.OpendedCalendar.OwnerName)
                                {
                                    if (Regex.IsMatch(title, "^[0-9A-Za-z_]*$"))
                                    {
                                        task.Title = newValue;
                                        return Responses.SuccessfulTaskEditing;
                                    }

                                    return Responses.InvalidTitle;
                                }

                                return Responses.YouHaveNotAccessToAddOrEditOrDeleteEventsOrTasks;

                            case "StartTime":
                                if (User.LoggedInUser.Name == CalendarClass.OpendedCalendar.OwnerName)
                                {
                                    if (task.EndTime > DateTime.Parse(newValue))
                                    {
                                        task.StartTime = DateTime.Parse(newValue);
                                        return Responses.SuccessfulTaskEditing;
                                    }

                                    return Responses.InvalidStartTime;
                                }
                                return Responses.YouHaveNotAccessToAddOrEditOrDeleteEventsOrTasks;

                            case "EndTime":
                                if (User.LoggedInUser.Name == CalendarClass.OpendedCalendar.OwnerName)
                                {
                                    if (task.StartTime < DateTime.Parse(newValue))
                                    {
                                        task.EndTime = DateTime.Parse(newValue);
                                        return Responses.SuccessfulTaskEditing;
                                    }

                                    return Responses.InvalidStartTime;
                                }
                                return Responses.YouHaveNotAccessToAddOrEditOrDeleteEventsOrTasks;
                        }

                        return Responses.InvalidCommand;
                    }

                    return Responses.InvalidTitle;
                }

                return Responses.YouHaveNotAccessToAddOrEditOrDeleteEventsOrTasks;
            }

            return Responses.NotExistedTask;
        }


        public string DeleteEvent(string title)
        {
            if (User.LoggedInUser.Name == CalendarClass.OpendedCalendar.OwnerName)
            {
                if (Regex.IsMatch(title, "^[0-9A-Za-z_]*$"))
                {
                    if (CalendarClass.OpendedCalendar.Events.Contains(Event.GetEventByTitle(title)))
                    {
                        CalendarClass.OpendedCalendar.Events.Remove(Event.GetEventByTitle(title));
                        Event.Events.Remove(Event.GetEventByTitle(title));
                        return Responses.SuccessfulEventDeleting;
                    }

                    return Responses.NotExistedEvent;
                }

                return Responses.InvalidTitle;
            }

            return Responses.YouHaveNotAccessToAddOrEditOrDeleteEventsOrTasks;
        }

        public string DeleteTask(string title)
        {
            if (User.LoggedInUser.Name == CalendarClass.OpendedCalendar.OwnerName)
            {
                if (Regex.IsMatch(title, "^[0-9A-Za-z_]*$"))
                {
                    if (CalendarClass.OpendedCalendar.Tasks.Contains(Task.GetTaskByTitle(title)))
                    {
                        CalendarClass.OpendedCalendar.Tasks.Remove(Task.GetTaskByTitle(title));
                        Task.Tasks.Remove(Task.GetTaskByTitle(title));
                        return Responses.SuccessfulTaskDeleting;
                    }

                    return Responses.NotExistedTask;
                }

                return Responses.InvalidTitle;
            }

            return Responses.YouHaveNotAccessToAddOrEditOrDeleteEventsOrTasks;
        }

        public string Back()
        {
            CalendarClass.OpendedCalendar = default;
            Menu.currentMenu = MenuName.MainMenu;
            return Responses.MainMenu;
        }
    }
}