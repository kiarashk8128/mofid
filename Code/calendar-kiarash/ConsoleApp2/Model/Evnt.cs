namespace ConsoleApp2.Model;

public class Evnt
{
    private string title;
    private string repeating;
    private bool hasMeetings;
    private int repeatNumber;
    private DateTime startDate;
    private DateTime endDate;
    private Calendar calendar;
    private string none;
    private static List<Evnt> evnts = new List<Evnt>();

    public Evnt(string calendarTitle,string title, DateTime startDate,DateTime endDate,string repeating, bool hasMeetings)
    {
        this.title = title;
        this.startDate = startDate;
        this.endDate = endDate;
        this.hasMeetings = hasMeetings;
        this.repeating = repeating;
        this.calendar = Calendar.getCalendarByTitle(calendarTitle);
        calendar.AddEvnt(this);
        evnts.Add(this);
    }
    
    public Evnt(string calendarTitle,string title, DateTime startDate,int repeatNumber, string repeating, bool hasMeetings)
    {
        this.title = title;
        this.startDate = startDate;
        this.repeatNumber = repeatNumber;
        this.repeating = repeating;
        this.hasMeetings = hasMeetings;
        this.calendar = Calendar.getCalendarByTitle(calendarTitle);
        calendar.AddEvnt(this);
        evnts.Add(this);
    }
    
    public Evnt(string calendarTitle,string title, DateTime startDate,string none, bool hasMeetings)
    {
        this.title = title;
        this.startDate = startDate;
        this.hasMeetings = hasMeetings;
        this.none = none;
        this.calendar = Calendar.getCalendarByTitle(calendarTitle);
        calendar.AddEvnt(this);
        evnts.Add(this);
    }

    public string Title
    {
        get => title;
        set => title = value ?? throw new ArgumentNullException(nameof(value));
    }

    public DateTime StartDate
    {
        get => startDate;
        set => startDate = value;
    }

    public DateTime EndDate
    {
        get => endDate;
        set => endDate = value;
    }

    public string Repeating
    {
        get => repeating;
        set => repeating = value;
    }

    public int RepeatNumber
    {
        get => repeatNumber;
        set => repeatNumber = value;
    }

    public bool HasMeetings
    {
        get => hasMeetings;
        set => hasMeetings = value;
    }
    public static Evnt getEventByTitle(string title)
    {
        foreach (var evnt in evnts)
        {
            if (evnt.title == title)
            {
                return evnt;
            }
        }
        return null;
    }
    public static void deleteEvnt(string title)
    {
        foreach(var evnt in new List<Evnt>(evnts))
        {
            if (evnt.title == title)
            {
                evnts.Remove(evnt);
            }
        }
    }
}