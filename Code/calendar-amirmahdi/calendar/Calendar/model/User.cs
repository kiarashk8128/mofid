using System.Collections.Generic;

namespace Calendar.model
{
    public class User
    {
        private string name;
        private string password;
        private bool isFirstLogin = true;
        private List<CalendarClass> calendars = new List<CalendarClass>();

        private static User loggedOnUser;
        private static List<User> users = new List<User>();

        public User(string name, string password)
        {
            Name = name;
            Password = password;
            users.Add(this);
        }

        public string Name
        {
            get => name;
            set => name = value;
        }

        public string Password
        {
            get => password;
            set => password = value;
        }

        public bool IsFirstLogin
        {
            get => isFirstLogin;
            set => isFirstLogin = value;
        }

        public static User LoggedInUser
        {
            get => loggedOnUser;
            set => loggedOnUser = value;
        }

        public List<CalendarClass> Calendars
        {
            get => calendars;
            set => calendars = value;
        }

        public static List<User> Users
        {
            get => users;
            set => users = value;
        }

        public static User GetUserByName(string name)
        {
            foreach (var user in User.users)
            {
                if (user.Name == name)
                {
                    return user;
                }
            }
            return null;
        }

        public void AddCalendar(CalendarClass calendarClass)
        {
            this.calendars.Add(calendarClass);
        }
        
    }
}