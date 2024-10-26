using System;
using System.Collections.Generic;
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
            // Check if any reminder is due
            ViewBag.IsReminderDue = CheckIfReminderDue();

            // Pass the notifications to the view
            return View(GetNotifications());
        }

        // Method to check if a reminder is due based on planting schedules or custom reminders
        public bool CheckIfReminderDue()
        {
            var today = DateTime.Today;
            var tomorrow = today.AddDays(1);

            // Retrieve planting schedules that have a planned planting date
            var schedules = _context.PlantingSchedules
                .Where(schedule => schedule.PlannedPlantingDate.HasValue)
                .ToList();

            // Check for planting and harvesting reminders due within 1 day
            bool isPlantingReminderDue = schedules.Any(schedule =>
                schedule.PlannedPlantingDate.HasValue &&
                schedule.PlannedPlantingDate.Value <= tomorrow &&
                schedule.PlannedPlantingDate.Value >= today);

            bool isHarvestReminderDue = schedules.Any(schedule =>
                schedule.PlannedPlantingDate.HasValue &&
                schedule.PlannedPlantingDate.Value.AddDays(schedule.DaysToHarvest) <= tomorrow &&
                schedule.PlannedPlantingDate.Value.AddDays(schedule.DaysToHarvest) >= today);

            // Check for custom user reminders due within 1 day
            var userReminders = _context.UserReminders.ToList();
            bool isCustomReminderDue = userReminders.Any(reminder =>
                reminder.ReminderDate <= tomorrow &&
                reminder.ReminderDate >= today);

            // Return true if any reminder is due within the next day
            return isPlantingReminderDue || isHarvestReminderDue || isCustomReminderDue;
        }

        // Action to add personal reminders
        [HttpPost]
        public IActionResult AddPersonalReminder(UserReminder reminder)
        {
            if (ModelState.IsValid)
            {
                // Save the reminder to the database
                _context.UserReminders.Add(reminder);
                _context.SaveChanges();

                // Redirect back to the notifications page
                return RedirectToAction("Index");
            }

            // If ModelState is not valid, return to the Index view with current notifications
            return View("Index", GetNotifications());
        }

        // Combined method to get notifications including user reminders
        private IEnumerable<NotificationViewModel> GetNotifications()
        {
            // Fetch planting schedules with planned planting dates
            var schedules = _context.PlantingSchedules
                .Where(schedule => schedule.PlannedPlantingDate.HasValue)
                .ToList();

            // Create a list of notifications from planting schedules
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

            // Fetch custom user reminders from the database
            var userReminders = _context.UserReminders.ToList();

            // Add user reminders to the notifications list
            notifications.AddRange(userReminders.Select(reminder => new NotificationViewModel
            {
                Title = reminder.ReminderType,
                Date = reminder.ReminderDate,
                ReminderNotes = reminder.ReminderNotes,
                IsCustomReminder = true // Indicate that this is a custom reminder
            }));

            return notifications;
        }
    }
}
