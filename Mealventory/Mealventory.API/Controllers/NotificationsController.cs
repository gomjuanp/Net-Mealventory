using Mealventory.Core.Interfaces;
using Mealventory.Core.Models;
using Mealventory.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace Mealventory.API.Controllers
{
    public class NotificationsController : Controller
    {
        private readonly NotificationManager _manager;

        public NotificationsController(NotificationManager manager)
        {
            _manager = manager;
        }

        public IActionResult AlertsList()
        {
            var notifications = _manager.GetAllNotifications();
            return View(notifications);
        }

        public IActionResult MarkRead(int id)
        {
            _manager.MarkAsRead(id);
            return RedirectToAction("AlertsList");
        }
    }
}
