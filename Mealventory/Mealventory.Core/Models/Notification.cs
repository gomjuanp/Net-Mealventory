//Owner: Sabaa Al-Gburi

using System;
using System.Collections.Generic;
using System.Text;

namespace Mealventory.Core.Models
{
    public class Notification
    {
        
        public string Message { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsRead { get; set; }
    }
}
