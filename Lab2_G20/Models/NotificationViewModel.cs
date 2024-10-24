namespace Lab2_G20.Models
{
    public class NotificationViewModel
    {
        public string Title { get; set; }
        public DateTime Date { get; set; } // Date for the notification
        public string ReminderNotes { get; set; }
        public bool IsCustomReminder { get; set; } // Indicates if this is a custom user reminder
    }
}
