using System.Text.RegularExpressions;
using ConsoleApp2.Model;
using ConsoleApp2.View;

namespace ConsoleApp2.Controller;

public class CalendarController
{
    public void run()
    {
        string command = Console.ReadLine();
        while (!command.Equals("back"))
        {
            if (Regex.IsMatch(command, "add event (.+)"))
            {
                Match match = commandMatcher(command, "add event (.+)");
                Console.WriteLine(addEvent(match).ToString());
            }
            else if (Regex.IsMatch(command, "add task (.+)"))
            {
                Match match = commandMatcher(command, "add task (.+)");
                Console.WriteLine(addTask(match).ToString());
            }
            else if (Regex.IsMatch(command, "delete event (.+)"))
            {
                Match match = commandMatcher(command, "delete event (.+)");
                Console.WriteLine(deleteEvent(match).ToString());
            }
            else if (Regex.IsMatch(command, "delete task (.+)"))
            {
                Match match = commandMatcher(command, "delete task (.+)");
                Console.WriteLine(deleteTask(match).ToString());
            }
            else if (Regex.IsMatch(command, "edit event (.+)"))
            {
                Match match = commandMatcher(command, "edit event (.+)");
                Console.WriteLine(editEvent(match).ToString());
            }
            else if (Regex.IsMatch(command, "edit task (.+)"))
            {
                Match match = commandMatcher(command, "edit task (.+)");
                Console.WriteLine(editTask(match).ToString());
            }
            // else if (Regex.IsMatch(command, "show (.+)"))
            // {
            //     Match match = commandMatcher(command, "show (.+)");
            //     showDate(match);
            // }
            
            else
            {
                Console.WriteLine("invalid command");
            }
            command=Console.ReadLine();
        }

        MenuController menu = new MenuController();
        menu.run();
    }

    // private void showDate(Match match)
    // {
    //     string date = match.Groups[1].Value;
    //     if (Regex.IsMatch(date, "(\\d\\d)-(\\d\\d)-(\\d\\d\\d\\d)"))
    //     {
    //         DateTime startRealDate = DateTime.Parse(date);
    //         foreach (Evnt evnt in evnts)
    //         {
    //             
    //         }
    //         
    //     }
    //
    //     Console.WriteLine("invalid date");
    //
    // }

    private CalendarMenuResponses editEvent(Match match)
    {
        string need = match.Groups[1].Value.Trim();
        String[] needed = Regex.Split(need, "\\s+");
        string title = needed[0];
        if (Regex.IsMatch(title, "(^\\w+$)"))
        {
            if (Evnt.getEventByTitle(title) != null)
            {
                if (needed[1] == "kindOfRepeat")
                {
                    string newRepeating = needed[2];
                    Evnt.getEventByTitle(title).Repeating = newRepeating;
                    return CalendarMenuResponses.EVENT_EDITED;
                }

                else if (needed[1] == "repeat")
                {
                    int repeatNumber = Int32.Parse(needed[2]);
                    ;
                    Evnt.getEventByTitle(title).Repeating = "d";
                    Evnt.getEventByTitle(title).RepeatNumber = repeatNumber;
                    return CalendarMenuResponses.EVENT_EDITED;
                }
                else if (needed[1] == "title")
                {
                    string newTitle = needed[2];
                    if (Regex.IsMatch(newTitle, "(^\\w+$)"))
                    {
                        if (Evnt.getEventByTitle(newTitle) == null)
                        {
                            Evnt.getEventByTitle(title).Title = newTitle;
                            return CalendarMenuResponses.EVENT_EDITED;
                        }

                        return CalendarMenuResponses.THERE_IS_ANOTHER_EVENT_WITH_THIS_TITLE;
                    }

                    return CalendarMenuResponses.INVALID_TITLE;
                }
                else if (needed[1] == "meet")
                {
                    string isMeeting = needed[1];
                    if (isMeeting == "t")
                    {
                        Evnt.getEventByTitle(title).HasMeetings = true;
                        return CalendarMenuResponses.EVENT_EDITED;
                    }
                    else if (isMeeting == "f")
                    {
                        Evnt.getEventByTitle(title).HasMeetings = false;
                        return CalendarMenuResponses.EVENT_EDITED;
                    }
                    else
                    {
                        return CalendarMenuResponses.INVALID_COMMAND;
                    }
                }
                else
                {
                    return CalendarMenuResponses.INVALID_FIELD;
                }
            }

            return CalendarMenuResponses.THERE_IS_NO_EVENT_WITH_THIS_TITLE;
        }

        return CalendarMenuResponses.INVALID_TITLE;
    }

    private CalendarMenuResponses editTask(Match match)
    {
        string need = match.Groups[1].Value.Trim();
        String[] needed = Regex.Split(need, "\\s+");
        string title = needed[0];
        if (Regex.IsMatch(title, "(^\\w+$)"))
        {
            if (Evnt.getEventByTitle(title) != null)
            {
                string field = needed[1];
                if (field == "meet")
                {
                    string isMeeting = needed[2];
                    if (isMeeting == "t")
                    {
                        Taski.getTaskByTitle(title).HasMeetings = true;
                        return CalendarMenuResponses.TASK_EDITED;
                    }
                    else if (isMeeting == "f")
                    {
                        Taski.getTaskByTitle(title).HasMeetings = false;
                        return CalendarMenuResponses.TASK_EDITED;
                    }
                    else
                    {
                        return CalendarMenuResponses.INVALID_COMMAND;
                    }
                }
                else if (field == "kindOfRepeat")
                {
                    string newRepeating = needed[2];
                    Taski.getTaskByTitle(title).Repeating = newRepeating;
                    return CalendarMenuResponses.TASK_EDITED;
                }
                else if (field == "repeat")
                {
                    int repeatNumber = Int32.Parse(needed[2]);
                    Taski.getTaskByTitle(title).Repeating = "d";
                    Taski.getTaskByTitle(title).RepeatNumber = repeatNumber;
                    return CalendarMenuResponses.TASK_EDITED;
                }
                else if (field == "title")
                {
                    string newTitle = needed[2];
                    if (Regex.IsMatch(newTitle, "(^\\w+$)"))
                    {
                        if (Taski.getTaskByTitle(newTitle) == null)
                        {
                            Taski.getTaskByTitle(title).Title = newTitle;
                            return CalendarMenuResponses.TASK_EDITED;
                        }

                        return CalendarMenuResponses.THERE_IS_ANOTHER_EVENT_WITH_THIS_TITLE;
                    }

                    return CalendarMenuResponses.INVALID_TITLE;
                }
                else if (field == "end time")
                {
                    string endTime = needed[2];
                    if (Regex.IsMatch(endTime, "((\\d\\d)-(\\d\\d))"))
                    {
                        DateTime realEndTime = DateTime.Parse(endTime);
                        Taski.getTaskByTitle(title).EndTime = realEndTime;
                        return CalendarMenuResponses.TASK_EDITED;
                    }
                    else
                    {
                        return CalendarMenuResponses.INVALID_END_TIME;
                    }
                }
                else if (field == "start time")
                {
                    string startTime = needed[2];
                    if (Regex.IsMatch(startTime, "((\\d\\d)-(\\d\\d))"))
                    {
                        DateTime realStartTime = DateTime.Parse(startTime);
                        Taski.getTaskByTitle(title).StartTime = realStartTime;
                        return CalendarMenuResponses.TASK_EDITED;
                    }

                    return CalendarMenuResponses.INVALID_START_TIME;
                }

                return CalendarMenuResponses.INVALID_FIELD;
            }

            return CalendarMenuResponses.THERE_IS_NO_TASK_WITH_THIS_TITLE;
        }

        return CalendarMenuResponses.INVALID_TITLE;
    }


    private CalendarMenuResponses deleteEvent(Match match)
    {
        string need = match.Groups[1].Value.Trim();
        String[] needed = Regex.Split(need, "\\s+");
        if (needed.Length == 1)
        {
            string title = needed[1];
            if (ProgramController.currentUserName ==
                Calendar.getCalendarByTitle(MenuController.currentCalendar).OwnerName)
            {
                if (Regex.IsMatch(title, "(^\\w+$)"))
                {
                    if (Evnt.getEventByTitle(title) != null)
                    {
                        Evnt.deleteEvnt(title);
                        return CalendarMenuResponses.EVENT_DELETED_SUCCESSFULLY;
                    }

                    return CalendarMenuResponses.THERE_IS_NO_EVENT_WITH_THIS_TITLE;
                }

                return CalendarMenuResponses.INVALID_TITLE;
            }

            return CalendarMenuResponses.YOU_DO_NOT_HAVE_ACCESS_TO_DO_THIS;
        }


        return CalendarMenuResponses.INVALID_COMMAND;
    }

    private CalendarMenuResponses deleteTask(Match match)
    {
        string need = match.Groups[1].Value.Trim();
        String[] needed = Regex.Split(need, "\\s+");
        if (needed.Length == 1)
        {
            string title = needed[1];
            if (ProgramController.currentUserName ==
                Calendar.getCalendarByTitle(MenuController.currentCalendar).OwnerName)
            {
                if (Regex.IsMatch(title, "(^\\w+$)"))
                {
                    if (Taski.getTaskByTitle(title) != null)
                    {
                        Taski.deleteTask(title);
                        return CalendarMenuResponses.TASK_DELETED_SUCCESSFULLY;
                    }

                    return CalendarMenuResponses.THERE_IS_NO_TASK_WITH_THIS_TITLE;
                }

                return CalendarMenuResponses.INVALID_TITLE;
            }

            return CalendarMenuResponses.YOU_DO_NOT_HAVE_ACCESS_TO_DO_THIS;
        }


        return CalendarMenuResponses.INVALID_COMMAND;
    }

    private CalendarMenuResponses addTask(Match match)
    {
        string need = match.Groups[1].Value.Trim();
        String[] needed = Regex.Split(need, "\\s+");
        if (needed.Length == 6)
        {
            string title = needed[0];
            string startTime = needed[1];
            string endTime = needed[2];
            string startDate = needed[3];
            string field = needed[4];
            string hasMeetings = needed[5];
            bool realHasMeetings = false;
            if (ProgramController.currentUserName ==
                Calendar.getCalendarByTitle(MenuController.currentCalendar).OwnerName)
            {
                if (Regex.IsMatch(title, "(^\\w+$)"))
                {
                    if (Taski.getTaskByTitle(title) == null)
                    {
                        if (Regex.IsMatch(startTime, "(\\d\\d)-(\\d\\d)"))
                        {
                            DateTime realStartTime = DateTime.Parse(startTime);
                            if (Regex.IsMatch(endTime, "((\\d\\d)-(\\d\\d))"))
                            {
                                DateTime realEndTime = DateTime.Parse(endTime);
                                if (Regex.IsMatch(startDate, "(\\d\\d)-(\\d\\d)-(\\d\\d\\d\\d)"))
                                {
                                    DateTime realStartDate = DateTime.Parse(startDate);
                                    if (Regex.IsMatch(field, "None"))
                                    {
                                        if (Regex.IsMatch(hasMeetings, ("t|f")))
                                        {
                                            if (hasMeetings.Equals("t"))
                                            {
                                                realHasMeetings = true;
                                            }
                                            else
                                            {
                                                realHasMeetings = false;
                                            }

                                            Taski taski = new Taski(MenuController.currentCalendar, title,
                                                realStartTime, realEndTime, realStartDate, realHasMeetings);
                                        }
                                    }
                                }

                                return CalendarMenuResponses.INVALID_START_DATE;
                            }

                            return CalendarMenuResponses.INVALID_END_TIME;
                        }

                        return CalendarMenuResponses.INVALID_START_TIME;
                    }

                    return CalendarMenuResponses.THERE_IS_ANOTHER_TASK_WITH_THIS_TITLE;
                }

                return CalendarMenuResponses.INVALID_TITLE;
            }

            return CalendarMenuResponses.YOU_DO_NOT_HAVE_ACCESS_TO_DO_THIS;
        }
        else if (needed.Length == 7)
        {
            string title = needed[0];
            string startTime = needed[1];
            string endTime = needed[2];
            string startDate = needed[3];
            string field = needed[4];
            string repeatMode = needed[5];
            string hasMeetings = needed[6];
            bool realHasMeetings = false;
            if (ProgramController.currentUserName ==
                Calendar.getCalendarByTitle(MenuController.currentCalendar).OwnerName)
            {
                if (Regex.IsMatch(title, "(^\\w+$)"))
                {
                    if (Taski.getTaskByTitle(title) == null)
                    {
                        if (Regex.IsMatch(startTime, "(\\d\\d)-(\\d\\d)"))
                        {
                            DateTime realStartTime = DateTime.Parse(startTime);
                            if (Regex.IsMatch(endTime, "((\\d\\d)-(\\d\\d))"))
                            {
                                DateTime realEndTime = DateTime.Parse(endTime);
                                if (Regex.IsMatch(startDate, "(\\d\\d)-(\\d\\d)-(\\d\\d\\d\\d)"))
                                {
                                    DateTime startRealDate = DateTime.Parse(startDate);
                                    if (Regex.IsMatch(field, "(\\d\\d)-(\\d\\d)-(\\d\\d\\d\\d)"))
                                    {
                                        DateTime endRealDate = DateTime.Parse(field);
                                        if (Regex.IsMatch(repeatMode, "(w|d|m)"))
                                        {
                                            string realRepeatMode = repeatMode;
                                            if (Regex.IsMatch(hasMeetings, ("t|f")))
                                            {
                                                if (hasMeetings.Equals("t"))
                                                {
                                                    realHasMeetings = true;
                                                }
                                                else
                                                {
                                                    realHasMeetings = false;
                                                }

                                                Taski taski = new Taski(MenuController.currentCalendar, title,
                                                    realStartTime, realEndTime,
                                                    startRealDate,
                                                    endRealDate, realRepeatMode, realHasMeetings);
                                                return CalendarMenuResponses.TASK_ADDED_SUCCESSFULLY;
                                            }
                                        }
                                    }
                                    else if (Regex.IsMatch(field, "(\\d+)"))
                                    {
                                        int repeatNumber = int.Parse(field);
                                        if (Regex.IsMatch(repeatMode, "(w|d|m)"))
                                        {
                                            string realRepeatMode = repeatMode;
                                            if (Regex.IsMatch(hasMeetings, ("t|f")))
                                            {
                                                if (hasMeetings.Equals("t"))
                                                {
                                                    realHasMeetings = true;
                                                }
                                                else
                                                {
                                                    realHasMeetings = false;
                                                }

                                                Taski taski = new Taski(MenuController.currentCalendar, title,
                                                    realStartTime, realEndTime,
                                                    startRealDate,
                                                    repeatNumber, realRepeatMode, realHasMeetings);
                                                return CalendarMenuResponses.TASK_ADDED_SUCCESSFULLY;
                                            }
                                        }
                                    }

                                    return CalendarMenuResponses.INVALID_FIELD;
                                }

                                return CalendarMenuResponses.INVALID_START_DATE;
                            }

                            return CalendarMenuResponses.INVALID_END_TIME;
                        }

                        return CalendarMenuResponses.INVALID_START_TIME;
                    }

                    return CalendarMenuResponses.THERE_IS_ANOTHER_TASK_WITH_THIS_TITLE;
                }

                return CalendarMenuResponses.INVALID_TITLE;
            }

            return CalendarMenuResponses.YOU_DO_NOT_HAVE_ACCESS_TO_DO_THIS;
        }

        return CalendarMenuResponses.INVALID_COMMAND;
    }

    private CalendarMenuResponses addEvent(Match match)
    {
        string need = match.Groups[1].Value.Trim();
        String[] needed = Regex.Split(need, "\\s+");
        if (needed.Length == 4)
        {
            string title = needed[0];
            string startDate = needed[1];
            string field = needed[2];
            string hasMeetings = needed[3];
            bool realHasMeetings = false;
            if (ProgramController.currentUserName ==
                Calendar.getCalendarByTitle(MenuController.currentCalendar).OwnerName)
            {
                if (Regex.IsMatch(title, "(^\\w+$)"))
                {
                    if (Evnt.getEventByTitle(title) == null)
                    {
                        if (Regex.IsMatch(startDate, "(\\d\\d)-(\\d\\d)-(\\d\\d\\d\\d)"))
                        {
                            DateTime startRealDate = DateTime.Parse(startDate);
                            if (Regex.IsMatch(field, "None"))
                            {
                                if (Regex.IsMatch(hasMeetings, ("t|f")))
                                {
                                    if (hasMeetings.Equals("t"))
                                    {
                                        realHasMeetings = true;
                                    }
                                    else
                                    {
                                        realHasMeetings = false;
                                    }

                                    Evnt evnt = new Evnt(MenuController.currentCalendar, title, startRealDate, field,
                                        realHasMeetings);
                                }
                            }
                        }

                        return CalendarMenuResponses.INVALID_START_DATE;
                    }

                    return CalendarMenuResponses.THERE_IS_ANOTHER_EVENT_WITH_THIS_TITLE;
                }

                return CalendarMenuResponses.INVALID_TITLE;
            }

            return CalendarMenuResponses.YOU_DO_NOT_HAVE_ACCESS_TO_DO_THIS;
        }
        else if (needed.Length == 5)
        {
            string title = needed[0];
            string startDate = needed[1];
            string field = needed[2];
            string repeatMode = needed[3];
            string hasMeetings = needed[4];
            bool realHasMeetings = false;
            if (ProgramController.currentUserName ==
                Calendar.getCalendarByTitle(MenuController.currentCalendar).OwnerName)
            {
                if (Regex.IsMatch(title, "(^\\w+$)"))
                {
                    if (Evnt.getEventByTitle(title) == null)
                    {
                        if (Regex.IsMatch(startDate, "(\\d\\d)-(\\d\\d)-(\\d\\d\\d\\d)"))
                        {
                            DateTime startRealDate = DateTime.Parse(startDate);
                            if (Regex.IsMatch(field, "(\\d\\d)-(\\d\\d)-(\\d\\d\\d\\d)"))
                            {
                                DateTime endRealDate = DateTime.Parse(field);
                                if (Regex.IsMatch(repeatMode, "(w|d|m)"))
                                {
                                    string realRepeatMode = repeatMode;
                                    if (Regex.IsMatch(hasMeetings, ("t|f")))
                                    {
                                        if (hasMeetings.Equals("t"))
                                        {
                                            realHasMeetings = true;
                                        }
                                        else
                                        {
                                            realHasMeetings = false;
                                        }

                                        Evnt evnt = new Evnt(MenuController.currentCalendar, title, startRealDate,
                                            endRealDate, realRepeatMode, realHasMeetings);
                                        return CalendarMenuResponses.EVENT_ADDED_SUCCESSFULLY;
                                    }
                                }
                            }
                            else if (Regex.IsMatch(field, "(\\d+)"))
                            {
                                int repeatNumber = int.Parse(field);
                                if (Regex.IsMatch(repeatMode, "(w|d|m)"))
                                {
                                    string realRepeatMode = repeatMode;
                                    if (Regex.IsMatch(hasMeetings, ("t|f")))
                                    {
                                        if (hasMeetings.Equals("t"))
                                        {
                                            realHasMeetings = true;
                                        }
                                        else
                                        {
                                            realHasMeetings = false;
                                        }

                                        Evnt evnt = new Evnt(MenuController.currentCalendar, title, startRealDate,
                                            repeatNumber, realRepeatMode, realHasMeetings);
                                        return CalendarMenuResponses.EVENT_ADDED_SUCCESSFULLY;
                                    }
                                }
                            }

                            return CalendarMenuResponses.INVALID_FIELD;
                        }

                        return CalendarMenuResponses.INVALID_START_DATE;
                    }

                    return CalendarMenuResponses.THERE_IS_ANOTHER_EVENT_WITH_THIS_TITLE;
                }

                return CalendarMenuResponses.INVALID_TITLE;
            }

            return CalendarMenuResponses.YOU_DO_NOT_HAVE_ACCESS_TO_DO_THIS;
        }

        return CalendarMenuResponses.INVALID_COMMAND;
    }


    private Match commandMatcher(string command, string pattern)
    {
        Match match = Regex.Match(command, pattern);
        return match;
    }
}