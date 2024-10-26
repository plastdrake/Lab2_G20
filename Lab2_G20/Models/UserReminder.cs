namespace Lab2_G20.Models
{
    public class UserReminder
    {
        public int Id { get; set; }
        public string ReminderType { get; set; }
        public DateTime ReminderDate { get; set; }
        public string ReminderNotes { get; set; }
    }
}
