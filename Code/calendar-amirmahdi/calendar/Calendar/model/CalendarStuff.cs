using System;
using System.Collections.Generic;

namespace Calendar.model
{
    public abstract class CalendarStuff
    {
        protected CalendarClass CalendarClass;
        protected string title;
        protected bool hasMeeting;
        protected DateTime startDate;
        protected DateTime? endDate = null;
        protected char repeatMode;
        protected int? repeatNo = null;
        

        public string Title
        {
            get => title;
            set => title = value;
        }

        public bool HasMeeting
        {
            get => hasMeeting;
            set => hasMeeting = value;
        }
        
        public DateTime StartDate
        {
            get => startDate;
            set => startDate = value;
        }

        public DateTime? EndDate
        {
            get => endDate;
            set => endDate = value;
        }

        public char RepeatMode
        {
            get => repeatMode;
            set => repeatMode = value;
        }

        public int? RepeatNo
        {
            get => repeatNo;
            set => repeatNo = value;
        }


    }
}