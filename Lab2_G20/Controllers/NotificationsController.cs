using Lab2_G20.Data;
using Lab2_G20.Models;
using Microsoft.AspNetCore.Mvc;

public class NotificationsController : Controller
{
    private readonly ApplicationDbContext _context;

    public NotificationsController(ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        // Get the schedules with planned planting dates
        var schedules = _context.PlantingSchedules
            .Where(schedule => schedule.PlannedPlantingDate.HasValue) // Ensure PlannedPlantingDate is not null
            .ToList(); // Execute the query and bring results into memory

        // Create notifications
        var notifications = schedules.SelectMany(schedule => new[]
        {
        // Reminder for upcoming planting
        new NotificationViewModel
        {
            Title = $"Plant {schedule.Crop}",
            Date = schedule.PlannedPlantingDate.Value.AddDays(-schedule.ReminderDaysBefore), // Reminder date for planting
            ReminderNotes = $"Remember to plant {schedule.Crop}!",
            IsCustomReminder = false // Set as needed
        },
        // Reminder for harvest
        new NotificationViewModel
        {
            Title = $"Harvest {schedule.Crop}",
            Date = schedule.PlannedPlantingDate.Value.AddDays(schedule.DaysToHarvest), // Harvest date
            ReminderNotes = $"Time to harvest {schedule.Crop}!",
            IsCustomReminder = false // Set as needed
        }
    }).ToList();

        return View(notifications); // Pass the view model to the view
    }

}
