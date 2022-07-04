namespace ConsoleApp2.Model;

public class User
{
    
    private string username;
    private string password;
    private static List<User> users = new List<User>();
  //  private List<string> calendars = new List<string>();
    private static List<Calendar> calendars = new List<Calendar>();

    public User(string username, string password)
    {
        this.username = username;
        this.password = password;
        users.Add(this);
        
    }

    public List<Calendar> Calendars
    {
        get => calendars;
        set => calendars = value ?? throw new ArgumentNullException(nameof(value));
    }

    public static User getUserByUsername(string username)
    {
        foreach (var user in users)
        {
            if (user.Username == username)
            {
                return user;
            }
        }
        return null;
        
    }

    public string Username
    {
        get => username;
        set => username = value ?? throw new ArgumentNullException(nameof(value));
    }

    public static List<User> getAllUsers()
    {
        return users;
    }

    public string Password
    {
        get => password;
        set => password = value ?? throw new ArgumentNullException(nameof(value));
    }

    public static void removeUser(string username)
    {
    //    var item = users.Find(x=>x.username == username);
      // users.Remove(item);
        foreach (var user in new List<User>(users))
        {
            if (user.Username == username)
            {
                users.Remove(user);
            }
        }

        
    }

    public static void removeCalendar(string title)
    {
        foreach (var calendar in new List<Calendar>(calendars))
        {
            if (Calendar.getCalendarByTitle !=null)
            {
                calendars.Remove(Calendar.getCalendarByTitle(title));
            }
        }
    }
}