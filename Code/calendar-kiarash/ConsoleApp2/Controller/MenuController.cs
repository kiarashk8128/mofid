using System.Text.RegularExpressions;
using ConsoleApp2.Model;
using ConsoleApp2.View;

namespace ConsoleApp2.Controller;

public class MenuController

{
    public static string currentCalendar;

    public void run()
    {
        string command = Console.ReadLine();
        while (!command.Equals("back"))
        {
            if (Regex.IsMatch(command, "create new calendar (.+)"))
            {
                Match match = commandMatcher(command, "create new calendar (.+)");
                Console.WriteLine(createCalendar(match).ToString());
            }
            else if (Regex.IsMatch(command, "open calendar (.+)"))
            {
                Match match = commandMatcher(command, "open calendar (.+)");
                Console.WriteLine(openCalendar(match).ToString());
                if (openCalendar(match).ToString() == MainMenuResponses.CALENDAR_OPENED_SUCCESSFULLY.ToString())
                {
                    CalendarController calendarController = new CalendarController();
                    calendarController.run();
                }
            }
            else if (Regex.IsMatch(command, "(enable|disable) calendar (.+)"))
            {
                Match match = commandMatcher(command, "(enable|disable) calendar (.+)");
                Console.WriteLine(enableOrDisableCalendar(match).ToString());
            }
            else if (Regex.IsMatch(command, "delete calendar (.+)"))
            {
                Match match = commandMatcher(command, "delete calendar (.+)");
                Console.WriteLine(deleteCalendar(match).ToString());
            }
            else if (Regex.IsMatch(command, "remove calendar (.+)"))
            {
                Match match = commandMatcher(command, "remove calendar (.+)");
                Console.WriteLine(removeCalendar(match).ToString());
            }
            else if (Regex.IsMatch(command, "edit calendar (.+)"))
            {
                Match match = commandMatcher(command, "edit calendar (.+)");
                Console.WriteLine(editCalendar(match).ToString());
            }
            else if (Regex.IsMatch(command, "share calendar (.+)"))
            {
                Match match = commandMatcher(command, "share calendar (.+)");
                Console.WriteLine(shareCalendar(match).ToString());
            }
            else if (Regex.IsMatch(command, "show calendars"))
            {
                showCalendars();
            }
            else if (Regex.IsMatch(command, "show enabled calendars"))
            {
                showEnabledCalendars();
            }
            else if (Regex.IsMatch(command, "logout"))
            {
                Console.WriteLine("logout successful");
                ProgramController program1 = new ProgramController();
                program1.run();
            }
            else
            {
                Console.WriteLine("invalid command");
            }

            command = Console.ReadLine();
        }

        ProgramController program = new ProgramController();
        program.run();
    }

    private MainMenuResponses createCalendar(Match match)
    {
        string title = match.Groups[1].Value.Trim();
        if (Regex.IsMatch(title, "(^\\w+$)"))
        {
            Calendar calendar = new Calendar(title, ProgramController.currentUserName, false);
            return MainMenuResponses.CALENDAR_CREATED_SUCCESSFULLY;
        }

        return MainMenuResponses.INVALID_TITLE;
    }

    private MainMenuResponses openCalendar(Match match)
    {
        string idStr = match.Groups[1].Value.Trim();
        if (Regex.IsMatch(idStr, "(\\d+)"))
        {
            int id = int.Parse(idStr);
            if (Calendar.getCalendarById(id) != null)
            {
                if (Calendar.getCalendarById(id).OwnerName == ProgramController.currentUserName)
                {
                    currentCalendar = Calendar.getCalendarById(id).Title;
                    return MainMenuResponses.CALENDAR_OPENED_SUCCESSFULLY;
                }

                return MainMenuResponses.YOU_HAVE_NO_CALENDAR_WITH_THIS_ID;
            }
        }

        return MainMenuResponses.THERE_IS_NO_CALENDAR_WITH_THIS_ID;
    }

    private MainMenuResponses enableOrDisableCalendar(Match match)
    {
        string idStr = match.Groups[2].Value.Trim();
        if (Regex.IsMatch(idStr, "(\\d+)"))
        {
            int id = int.Parse(idStr);
            if (Calendar.getCalendarById(id) != null)
            {
                if (Calendar.getCalendarById(id).OwnerName == ProgramController.currentUserName)
                {
                    if (match.Value.Contains("enable"))
                    {
                        Calendar.getCalendarById(id).setEnabled(true);
                        return MainMenuResponses.CALENDAR_ENABLED_SUCCESSFULLY;
                    }
                    else
                    {
                        Calendar.getCalendarById(id).setEnabled(false);
                        return MainMenuResponses.CALENDAR_DISABLED_SUCCESSFULLY;
                    }
                }

                return MainMenuResponses.YOU_HAVE_NO_CALENDAR_WITH_THIS_ID;
            }
        }

        return MainMenuResponses.THERE_IS_NO_CALENDAR_WITH_THIS_ID;
    }

    private MainMenuResponses deleteCalendar(Match match)
    {
        string idStr = match.Groups[1].Value.Trim();
        if (Regex.IsMatch(idStr, "(\\d+)"))
        {
            int id = int.Parse(idStr);
            if (Calendar.getCalendarById(id) != null)
            {
                if (User.getUserByUsername(ProgramController.currentUserName).Calendars
                    .Contains(Calendar.getCalendarById(id)))
                {
                    {
                        if (Calendar.getCalendarById(id).OwnerName == ProgramController.currentUserName)
                        {
                            Calendar.deleteCalendar(Calendar.getCalendarById(id).Title);
                            return MainMenuResponses.CALENDAR_DELETED;
                        }

                        return MainMenuResponses.YOU_DO_NOT_HAVE_ACCESS_TO_DELETE_THIS_CALENDAR;
                    }
                }

                return MainMenuResponses.YOU_HAVE_NO_CALENDAR_WITH_THIS_ID;
            }

            return MainMenuResponses.THERE_IS_NO_CALENDAR_WITH_THIS_ID;
        }

        return MainMenuResponses.INVALID_ID;
    }

    private MainMenuResponses removeCalendar(Match match)
    {
        string idStr = match.Groups[1].Value.Trim();
        if (Regex.IsMatch(idStr, "(\\d+)"))
        {
            int id = int.Parse(idStr);
            if (Calendar.getCalendarById(id) != null)
            {
                if (User.getUserByUsername(ProgramController.currentUserName).Calendars
                    .Contains(Calendar.getCalendarById(id)))
                {
                    if (Calendar.getCalendarById(id).OwnerName == ProgramController.currentUserName)
                    {
                        Console.WriteLine("Do you want to delete the calendar");
                        string answer = Console.ReadLine();
                        if (answer.Equals("yes"))
                        {
                            Calendar.deleteCalendar(Calendar.getCalendarById(id).Title);
                        }
                        else
                        {
                            Console.WriteLine("ok");
                        }
                    }

                    User.removeCalendar(Calendar.getCalendarById(id).Title);
                    return MainMenuResponses.CALENDAR_REMOVED;
                }

                return MainMenuResponses.YOU_HAVE_NO_CALENDAR_WITH_THIS_ID;
            }

            return MainMenuResponses.THERE_IS_NO_CALENDAR_WITH_THIS_ID;
        }

        return MainMenuResponses.INVALID_ID;
    }

    private MainMenuResponses editCalendar(Match match)
    {
        string need = match.Groups[1].Value.Trim();
        String[] needed = Regex.Split(need, "\\s+");

        string title = needed[1];
        if (Regex.IsMatch(title, "(^\\w+$)"))
        {
            if (Regex.IsMatch(needed[0], "\\d+"))
            {
                int id = int.Parse(needed[0]);
                if (Calendar.getCalendarById(id) != null)
                {
                    if (User.getUserByUsername(ProgramController.currentUserName).Calendars
                        .Contains(Calendar.getCalendarById(id)))
                    {
                        if (Calendar.getCalendarById(id).OwnerName == ProgramController.currentUserName)
                        {
                            Calendar.getCalendarById(id).Title = title;
                            return MainMenuResponses.CALENDAR_TITLE_EDITED;
                        }

                        return MainMenuResponses.YOU_DO_NOT_HAVE_ACCESS_TO_EDIT_THIS_CALENDAR;
                    }

                    return MainMenuResponses.YOU_HAVE_NO_CALENDAR_WITH_THIS_ID;
                }

                return MainMenuResponses.THERE_IS_NO_CALENDAR_WITH_THIS_ID;
            }

            return MainMenuResponses.INVALID_ID;
        }

        return MainMenuResponses.INVALID_TITLE;
    }

    private MainMenuResponses shareCalendar(Match match)
    {
        string needed = match.Groups[1].Value.Trim();
        String[] needs = Regex.Split(needed, "\\s+");
        string idStr = needs[0];
        List<string> userNames = new List<string>();
        if (Regex.IsMatch(idStr, "(\\d+)"))
        {
            int id = int.Parse(idStr);

            if (Calendar.getCalendarById(id) != null)
            {
                if (User.getUserByUsername(ProgramController.currentUserName).Calendars
                    .Contains(Calendar.getCalendarById(id)))
                {
                    if (Calendar.getCalendarById(id).OwnerName == ProgramController.currentUserName)
                    {
                        for (int i = 1; i < needs.Length; i++)
                        {
                            if (User.getUserByUsername(needs[i]) == null)
                            {
                                return MainMenuResponses.NO_USER_EXISTS_WITH_THIS_USERNAME;
                            }

                            userNames.Add(needs[i]);
                        }

                        for (int j = 0; j < userNames.Count; j++)
                        {
                            User.getUserByUsername(userNames[j]).Calendars.Add(Calendar.getCalendarById(id));
                        }
                    }

                    return MainMenuResponses.YOU_DO_NOT_HAVE_ACCESS_TO_SHARE_THIS_CALENDAR;
                }

                return MainMenuResponses.YOU_HAVE_NO_CALENDAR_WITH_THIS_ID;
            }

            return MainMenuResponses.THERE_IS_NO_CALENDAR_WITH_THIS_ID;
        }

        return MainMenuResponses.INVALID_ID;
    }

    private void showCalendars()
    {
        if (User.getUserByUsername(ProgramController.currentUserName).Calendars.Count == 0)
        {
            Console.WriteLine("nothing");
            return;
        }

        int counter = 0;
        foreach (Calendar calendar in User.getUserByUsername(ProgramController.currentUserName).Calendars)
        {
            if (User.getUserByUsername(ProgramController.currentUserName).Username == calendar.OwnerName)
            {
                counter++;
                Console.WriteLine(calendar.Title + " " + calendar.CalendarId);
            }
        }

        if (counter == 0)
        {
            Console.WriteLine("nothing");
        }
    }

    private void showEnabledCalendars()
    {
        if (User.getUserByUsername(ProgramController.currentUserName).Calendars.Count == 0)
        {
            Console.WriteLine("nothing");
            return;
        }

        int counter = 0;
        foreach (Calendar calendar in User.getUserByUsername(ProgramController.currentUserName).Calendars)
        {
            if (User.getUserByUsername(ProgramController.currentUserName).Username == calendar.OwnerName &&
                calendar.isCalendarEnabled())
            {
                counter++;
                Console.WriteLine(calendar.Title + " " + calendar.CalendarId);
            }
        }

        if (counter == 0)
        {
            Console.WriteLine("nothing");
        }
    }


    private Match commandMatcher(string command, string pattern)
    {
        Match match = Regex.Match(command, pattern);
        return match;
    }
}