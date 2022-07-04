using System;

namespace Calendar.model
{
    public class Responses
    {
        public static readonly string InvalidCommand = "invalid command!";
        public static readonly string SuccessfulRegistering = "register successful!";
        public static readonly string InvalidUserName = "invalid username!";
        public static readonly string ExistedUser = "a user exists with this username";
        public static readonly string NotExistedUser = "no user exists with this username";
        public static readonly string InvalidPassword = "invalid password!";
        public static readonly string WrongPassword = "password is wrong!";
        public static readonly string WeakPassword = "password is weak!";
        public static readonly string SuccessfulLogin = "login successful!";
        public static readonly string SuccessfulPasswordChanging = "password changed successfully!";
        public static readonly string InvalidOldPassword = "invalid old password!";
        public static readonly string InvalidNewPassword = "invalid new password!";
        public static readonly string NewWeakPassword = "new password is weak!";
        public static readonly string SuccessfulRemoving = "removed successfully!";
        public static readonly string SuccessfulCalendarCreating = "calendar created successfully!";
        public static readonly string InvalidTitle = "invalid title!";
        public static readonly string InvalidNewTitle = "invalid new title!";
        public static readonly string SuccessfulCalendarOpening = "calendar opened successfully!";
        public static readonly string NotExistedCalendar = "there is no calendar with this ID!";
        public static readonly string YouHaveNotThisCalendar = "you have no calendar with this ID!";
        public static readonly string SuccessfulCalendarEnabling = "calendar enabled successfully!";
        public static readonly string SuccessfulCalendarDisabling = "calendar disabled successfully!";
        public static readonly string SuccessfulCalendarDeleting = "calendar deleted!";
        public static readonly string YouHaveNotAccessToDeleteThisCalendar = "you don't have access to delete this calendar!";
        public static readonly string SuccessfulCalendarRemoving = "calendar removed!";
        public static readonly string AskToRemoving = "do you want to delete this calendar?(yes/no)";
        public static readonly string NO = "OK!";
        public static readonly string Nothing = "nothing";
        public static readonly string SuccessfulTitleEditing = "calendar title edited!";
        public static readonly string YouHaveNotAccessToEditThisCalendar = "you don't have access to edit this calendar!";
        public static readonly string SuccessfulCalendarSharing = "calendar shared!";
        public static readonly string YouHaveNotAccessToShareThisCalendar = "you don't have access to share this calendar!";
        public static readonly string InvalidDate = "date is invalid!";
        public static readonly string SuccessfulLogout = "logout successful";
        public static readonly string ShowEvents = "events of this calendar:";
        public static readonly string ShowTasks = "tasks of this calendar:";
        public static readonly string SuccessfulEventAdding = "event added successfully!";
        public static readonly string SuccessfulTaskAdding = "task added successfully!";
        public static readonly string InvalidStartDate = "invalid start date!";
        public static readonly string InvalidStartTime = "invalid start time!";
        public static readonly string InvalidEndDate = "invalid end date!";
        public static readonly string InvalidEndTime = "invalid end time!";
        public static readonly string ExistedEvent = "there is another event with this title!";
        public static readonly string ExistedTask = "there is another task with this title!";
        public static readonly string NotExistedEvent = "there is no event with this title!";
        public static readonly string NotExistedTask = "there is no task with this title!";
        public static readonly string SuccessfulEventEditing = "event edited!";
        public static readonly string SuccessfulTaskEditing = "task edited!";
        public static readonly string SuccessfulEventDeleting = "event deleted successfully!";
        public static readonly string SuccessfulTaskDeleting = "task deleted successfully!";
        public static readonly string YouHaveNotAccessToAddOrEditOrDeleteEventsOrTasks = "you don't have access to do this!";
        public static readonly string RegisterMenu = "you are in register menu!";
        public static readonly string MainMenu = "you are in main menu!";
        public static readonly string CalendarMenu = "you are in calendar menu!";
    }
}