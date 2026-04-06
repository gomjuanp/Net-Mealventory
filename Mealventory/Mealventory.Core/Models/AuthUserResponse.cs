// Owner 1: "Daniel Bajenov" has added 100% of the code in this file
// Principal Author: Daniel Bajenov
// Description: DTO returned after successful authentication containing basic user info.
using System;
using System.Collections.Generic;
using System.Text;

namespace Mealventory.Core.Models
{
    /// <summary>
    /// Authentication response DTO containing public user information.
    /// </summary>
    public class AuthUserResponse
    {
        /// <summary>
        /// The user's id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The user's username.
        /// </summary>
        public string Username { get; set; } = string.Empty;

        /// <summary>
        /// The user's email address.
        /// </summary>
        public string Email { get; set; } = string.Empty;
    }
}