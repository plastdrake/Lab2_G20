namespace Lab2_G20.Models
{
    public class UserReminder
    {
        public int Id { get; set; }
        public string ReminderType { get; set; }  // Fertilizing, Watering, Pest Control, etc.
        public DateTime ReminderDate { get; set; }
        public string ReminderNotes { get; set; }  // Changed Notes to ReminderNotes
    }
}
