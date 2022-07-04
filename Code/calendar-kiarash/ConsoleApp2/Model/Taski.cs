namespace ConsoleApp2.Model;

public class Taski
{
    private string title;
    private string repeating;
    private bool hasMeetings;
    private int repeatNumber;
    private DateTime startDate;
    private DateTime startTime;
    private DateTime endTime;
    private DateTime endDate;
    private Calendar calendar;
    private static List<Taski> taskis = new List<Taski>();

    public Taski(string calendarTitle, string title, DateTime startTime, DateTime endTime, DateTime startDate,
        DateTime endDate, string repeating, bool hasMeetings)
    {
        this.calendar = Calendar.getCalendarByTitle(calendarTitle);
        this.title = title;
        this.startDate = startDate;
        this.startTime = startTime;
        this.endTime = endTime;
        this.endDate = endDate;
        this.hasMeetings = hasMeetings;
        calendar.AddTaski(this);
        taskis.Add(this);
    }


    public Taski(string calendarTitle, string title, DateTime startTime, DateTime endTime, DateTime startDate,
        int repeatNumber,
        string repeating, bool hasMeetings)
    {
        this.calendar = Calendar.getCalendarByTitle(calendarTitle);
        this.title = title;
        this.startTime = startTime;
        this.endTime = endTime;
        this.startDate = startDate;
        this.repeating = repeating;
        this.repeatNumber = repeatNumber;
        this.hasMeetings = hasMeetings;
        calendar.AddTaski(this);
        taskis.Add(this);
    }


    public Taski(string calendarTitle, string title, DateTime startTime, DateTime endDate, DateTime startDate,
        bool hasMeetings)
    {
        this.calendar = Calendar.getCalendarByTitle(calendarTitle);
        this.title = title;
        this.startDate = startDate;
        this.startTime = startTime;
        this.endDate = endDate;
        this.hasMeetings = hasMeetings;
        calendar.AddTaski(this);
        taskis.Add(this);
    }

    public string Title
    {
        get => title;
        set => title = value ?? throw new ArgumentNullException(nameof(value));
    }

    public DateTime EndTime
    {
        get => endTime;
        set => endTime = value;
    }

    public DateTime StartTime
    {
        get => startTime;
        set => startTime = value;
    }

    public DateTime EndDate
    {
        get => endDate;
        set => endDate = value;
    }

    public bool HasMeetings
    {
        get => hasMeetings;
        set => hasMeetings = value;
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

    public static Taski getTaskByTitle(string title)
    {
        foreach (var taski in taskis)
        {
            if (taski.title == title)
            {
                return taski;
            }
        }

        return null;
    }


    public static void deleteTask(string title)
    {
        foreach (var taski in new List<Taski>(taskis))
        {
            if (taski.title == title)
            {
                taskis.Remove(taski);
            }
        }
    }
}