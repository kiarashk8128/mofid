using System;
using System.Text.RegularExpressions;
using Calendar.model;
using Calendar.view;

namespace Calendar.controller
{
    
    public class ProgramController
    {
        private delegate Match Matcher(string command, string pattern);
        private RegisterController registerController = RegisterController.GetInstance();
        private MainController mainController = MainController.GetInstance();
        private CalendarController calendarController = CalendarController.GetInstance();
        private static ProgramController instance;
        private ProgramController(){}

        public static ProgramController GetInstance()
        {
            return instance ?? (instance = new ProgramController());
        }


        public void Run()
        {
            do
            {
                string command = Console.ReadLine();
                Console.WriteLine(Proccess(command));
            } while (true);
        }

        private string Proccess(string command)
        {
            Matcher matcher = Tools.PatternMatcher;
            command = command.Trim();

            if (command.Equals("exit")) Environment.Exit(0);

            if (command.StartsWith("register")  && Menu.currentMenu == MenuName.RegisterMenu)
            {
                if (matcher(command, "register (\\S+) (\\S+)").Success)
                {
                    Match match = matcher(command, "register (\\S+) (\\S+)");
                    return registerController.Register(match.Groups[1].Value, match.Groups[2].Value);
                }

                return Responses.InvalidCommand;
            }

            if (command.StartsWith("login") && Menu.currentMenu == MenuName.RegisterMenu)
            {
                if (matcher(command, "login (\\S+) (\\S+)").Success)
                {
                    Match match = matcher(command, "login (\\S+) (\\S+)");
                    return registerController.Login(match.Groups[1].Value, match.Groups[2].Value);
                }

                return Responses.InvalidCommand;
            }

            if (command.StartsWith("change") && Menu.currentMenu == MenuName.RegisterMenu)
            {
                if (matcher(command, "change password (\\S+) (\\S+) (\\S+)").Success)
                {
                    Match match = matcher(command, "change password (\\S+) (\\S+) (\\S+)");
                    return registerController.ChangePassword(match.Groups[1].Value, match.Groups[2].Value,
                        match.Groups[3].Value);
                }

                return Responses.InvalidCommand;
            }

            if (command.StartsWith("remove"))
            {
                if (command.StartsWith("remove calendar"))
                {
                    if (matcher(command, "remove calendar (\\d+)").Success && Menu.currentMenu == MenuName.MainMenu)
                    {
                        Match match = matcher(command, "remove calendar (\\d+)");
                        return mainController.Remove(int.Parse(match.Groups[1].Value));
                    }

                    return Responses.InvalidCommand;
                }

                if (matcher(command, "remove (\\S+) (\\S+)").Success && Menu.currentMenu == MenuName.RegisterMenu)
                {
                    Match match = matcher(command, "remove (\\S+) (\\S+)");
                    return registerController.Remove(match.Groups[1].Value, match.Groups[2].Value);
                }

                return Responses.InvalidCommand;
            }

            if (command.StartsWith("show"))
            {
                if (command.Equals("show all users") && Menu.currentMenu == MenuName.RegisterMenu) return registerController.ShowAllUsers();
                if (command.Equals("show calendars") && Menu.currentMenu == MenuName.MainMenu) return mainController.ShowCalendars();
                if (command.Equals("show enabled calendars") && Menu.currentMenu == MenuName.MainMenu) return mainController.ShowEnabledCalendars();
                if (command.Equals("show events") && Menu.currentMenu == MenuName.CalendarMenu) return calendarController.ShowEvents();
                if (command.Equals("show tasks") && Menu.currentMenu == MenuName.CalendarMenu) return calendarController.ShowTasks();
                if (matcher(command, "show (\\d\\d-\\d\\d-\\d\\d\\d\\d)").Success && Menu.currentMenu == MenuName.MainMenu)
                {
                    Match match = matcher(command, "show (\\d\\d-\\d\\d-\\d\\d\\d\\d)");
                    return mainController.ShowDate(match.Groups[1].Value);
                }
            }

            if (command.StartsWith("create") && Menu.currentMenu == MenuName.MainMenu)
            {
                if (matcher(command, "create new calendar (\\S+)").Success)
                {
                    Match match = matcher(command, "create new calendar (\\S+)");
                    return mainController.Create(User.LoggedInUser.Name, match.Groups[1].Value);
                }

                return Responses.InvalidCommand;
            }

            if (command.StartsWith("open") && Menu.currentMenu == MenuName.MainMenu)
            {
                if (matcher(command, "open calendar (\\S+)").Success)
                {
                    Match match = matcher(command, "open calendar (\\S+)");
                    return mainController.Open(int.Parse(match.Groups[1].Value));
                }

                return Responses.InvalidCommand;
            }

            if (command.StartsWith("enable") && Menu.currentMenu == MenuName.MainMenu)
            {
                if (matcher(command, "enable calendar (\\S+)").Success)
                {
                    Match match = matcher(command, "enable calendar (\\S+)");
                    return mainController.Enable(int.Parse(match.Groups[1].Value));
                }

                return Responses.InvalidCommand;
            }

            if (command.StartsWith("disable") && Menu.currentMenu == MenuName.MainMenu)
            {
                if (matcher(command, "disable calendar (\\S+)").Success)
                {
                    Match match = matcher(command, "disable calendar (\\S+)");
                    return mainController.Disable(int.Parse(match.Groups[1].Value));
                }

                return Responses.InvalidCommand;
            }

            if (command.StartsWith("edit"))
            {
                if (command.StartsWith("edit calendar") && Menu.currentMenu == MenuName.MainMenu)
                {
                    if (matcher(command, "edit calendar (\\S+) (\\S+)").Success)
                    {
                        Match match = matcher(command, "edit calendar (\\S+) (\\S+)");
                        return mainController.Edit(int.Parse(match.Groups[1].Value), match.Groups[2].Value);
                    }

                    return Responses.InvalidCommand;
                }

                if (command.StartsWith("edit event"))
                {
                    if (matcher(command, "edit event (\\S+) (\\S+) (\\S+)").Success && Menu.currentMenu == MenuName.CalendarMenu)
                    {
                        Match match = matcher(command, "edit event (\\S+) (\\S+) (\\S+)");
                        return calendarController.EditEvent(match.Groups[1].Value, match.Groups[2].Value,
                            match.Groups[3].Value);
                    }

                    return Responses.InvalidCommand;
                }

                if (matcher(command, "edit task (\\S+) (\\S+) (\\S+)").Success && Menu.currentMenu == MenuName.CalendarMenu)
                {
                    Match match = matcher(command, "edit task (\\S+) (\\S+) (\\S+)");
                    return calendarController.EditTask(match.Groups[1].Value, match.Groups[2].Value,
                        match.Groups[3].Value);
                }

                return Responses.InvalidCommand;
            }

            if (command.StartsWith("delete"))
            {
                if (command.StartsWith("delete calendar") && Menu.currentMenu == MenuName.MainMenu)
                {
                    if (matcher(command, "delete calendar (\\S+)").Success)
                    {
                        Match match = matcher(command, "delete calendar (\\S+)");
                        return mainController.Delete(int.Parse(match.Groups[1].Value));
                    }

                    return Responses.InvalidCommand;
                }

                if (command.StartsWith("delete event"))
                {
                    if (matcher(command, "delete event (\\S+)").Success && Menu.currentMenu == MenuName.CalendarMenu)
                    {
                        Match match = matcher(command, "delete event (\\S+)");
                        return calendarController.DeleteEvent(match.Groups[1].Value);
                    }

                    return Responses.InvalidCommand;
                }

                if (matcher(command, "delete task (\\S+)").Success && Menu.currentMenu == MenuName.CalendarMenu)
                {
                    Match match = matcher(command, "delete task (\\S+)");
                    return calendarController.DeleteTask(match.Groups[1].Value);
                }

                return Responses.InvalidCommand;
            }

            if (command.StartsWith("share") && Menu.currentMenu == MenuName.MainMenu)
            {
                if (matcher(command, "share calendar (\\S+) (.+)").Success)
                {
                    Match match = matcher(command, "share calendar (\\S+) (.+)");
                    return mainController.Share(int.Parse(match.Groups[1].Value), match.Groups[2].Value);
                }

                return Responses.InvalidCommand;
            }

            if (command.Equals("logout") && Menu.currentMenu == MenuName.MainMenu) return mainController.Logout();

            if (command.StartsWith("add") && Menu.currentMenu == MenuName.CalendarMenu)
            {
                if (command.StartsWith("add event"))
                {
                    if (matcher(command, "add event (\\S+) (\\S+) (\\S+) (\\S+) (\\S+)").Success)
                    {
                        Match match = matcher(command, "add event (\\S+) (\\S+) (\\S+) (\\S+) (\\S+)");
                        return calendarController.AddEvent(match.Groups[1].Value, match.Groups[2].Value,
                            match.Groups[3].Value, char.Parse(match.Groups[4].Value), match.Groups[5].Value);
                    }

                    return Responses.InvalidCommand;
                }

                if (matcher(command, "add task (\\S+) (\\S+) (\\S+) (\\S+) (\\S+) (\\S+) (\\S+)").Success)
                {
                    Match match = matcher(command, "add task (\\S+) (\\S+) (\\S+) (\\S+) (\\S+) (\\S+) (\\S+)");
                    return calendarController.AddTask(match.Groups[1].Value, match.Groups[2].Value,
                        match.Groups[3].Value, match.Groups[4].Value, match.Groups[5].Value,
                        char.Parse(match.Groups[6].Value), match.Groups[7].Value);
                }

                return Responses.InvalidCommand;
            }

            if (command.Equals("back") && Menu.currentMenu == MenuName.CalendarMenu) return calendarController.Back();

            return Responses.InvalidCommand;
        }
    }
}