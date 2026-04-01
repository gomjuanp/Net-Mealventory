using System;
using System.Collections.Generic;
using System.Text;

namespace Mealventory.Core.Models
{
    public class AuthUserResponse
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }
}