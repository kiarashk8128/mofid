namespace ConsoleApp2.Model;

public class Calendar
{
    private string title;
    private string ownerName;
    private int id;
    private static int calendarIdCounter = 1;
  
    private int calendarId;
    private bool isEnabled;
    //private bool isOpened;
    private static List<Calendar> calendars = new List<Calendar>();
    private  List<Evnt> evnts = new List<Evnt>();
    private List<Taski> taskis = new List<Taski>();

    public Calendar(string title, string owner,bool isEnabled)
    {
        this.title = title;
        OwnerName = owner;
        this.isEnabled = isEnabled;
     //   this.isOpened = false;
        CalendarId = calendarIdCounter;
        calendarIdCounter++;
        User.getUserByUsername(owner).Calendars.Add(this);
        calendars.Add(this);
        
    }

    public string OwnerName
    {
        get => ownerName;
        set => ownerName = value ?? throw new ArgumentNullException(nameof(value));
    }
    public int CalendarId
    {
        get => calendarId;
        set => calendarId = value;
    }

    public string Title
    {
        get => title;
        set => title = value ?? throw new ArgumentNullException(nameof(value));
    }

    public bool isCalendarEnabled()
    {
        return this.isEnabled;
    }

   
    

    public void setEnabled(bool isEnabled)
    {
        this.isEnabled = isEnabled;
    }

    public static Calendar getCalendarByTitle(string title)
    {
        foreach (var calendar in calendars)
        {
            if (calendar.title == title)
            {
               return calendar;
            }
        }
        return null;
    }
    public static Calendar getCalendarById(int id)
    {
        foreach (Calendar calendar in calendars)
        {
            if (calendar.calendarId== id)
            {
                return calendar;
            }
        }
        return null;
    }

    public List<Evnt> Events
    {
        get => evnts;
    }
    
    public void AddEvnt(Evnt evnt)
    {
        this.evnts.Add(evnt);
    }
    
    public void AddTaski(Taski taski)
    {
        this.taskis.Add(taski);
    }
    public static void deleteCalendar(string title)
    {
        //    var item = users.Find(x=>x.username == username);
        // users.Remove(item);
        foreach (var calendar in new List<Calendar>(calendars))
        {
            if (calendar.title == title)
            {
                calendars.Remove(calendar);
            }
        }

        
    }
}