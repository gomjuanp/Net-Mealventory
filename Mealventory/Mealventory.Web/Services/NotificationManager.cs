//Owner: Sabaa Al-Gburi

using Mealventory.Core.Models;

namespace Mealventory.Web.Services
{
    public class NotificationManager
    {
        private List<Notification> notifications = new List<Notification>();

        public List<Notification> GetAll()
        {
            return notifications;
        }

        public void AddNotification(string message)
        {
            notifications.Add(new Notification
            {
                Message = message,
                CreatedDate = DateTime.Now,
                IsRead = false
            });
        }

        public void MarkAsRead(Notification n)
        {
            n.IsRead = true;
        }
    }
}
