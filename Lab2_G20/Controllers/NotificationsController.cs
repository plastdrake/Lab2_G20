using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Lab2_G20.Data;
using Lab2_G20.Models;

namespace Lab2_G20.Controllers
{
    public class NotificationsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public NotificationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Index action to display all notifications
        public IActionResult Index()
        {
            // Fetch planting schedules from the database
            var schedules = _context.PlantingSchedules
                .Where(schedule => schedule.PlannedPlantingDate.HasValue)
                .ToList();

            // Generate notifications for planting and harvest
            var notifications = schedules.SelectMany(schedule => new[]
            {
                new NotificationViewModel
                {
                    Title = $"Plant {schedule.Crop}",
                    Date = schedule.PlannedPlantingDate.Value,
                    ReminderNotes = $"Remember to plant {schedule.Crop}!",
                    IsCustomReminder = false
                },
                new NotificationViewModel
                {
                    Title = $"Harvest {schedule.Crop}",
                    Date = schedule.PlannedPlantingDate.Value.AddDays(schedule.DaysToHarvest),
                    ReminderNotes = $"Time to harvest {schedule.Crop}!",
                    IsCustomReminder = false
                }
            }).ToList();

            // Calculate if any crop is within the ReminderDaysBefore range
            ViewBag.IsReminderDue = CheckIfReminderDue();

            // Pass the notifications to the view
            return View(notifications);
        }

        // Method to check if a reminder is due based on planting schedules
        public bool CheckIfReminderDue()
        {
            var today = DateTime.Today;

            // Retrieve planting schedules that have a planned planting date
            var schedules = _context.PlantingSchedules
                .Where(schedule => schedule.PlannedPlantingDate.HasValue)
                .ToList();

            // Determine if any crop is within the reminder period
            return schedules.Any(schedule =>
                schedule.PlannedPlantingDate.HasValue &&
                today >= schedule.PlannedPlantingDate.Value.AddDays(-schedule.ReminderDaysBefore) &&
                today < schedule.PlannedPlantingDate.Value
            );
        }
    }
}
