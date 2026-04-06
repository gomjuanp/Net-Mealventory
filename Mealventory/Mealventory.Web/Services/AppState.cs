// Owner 1: "Juan Pablo Ordonez Gomez" has added 80% of the code in this file
// Owner 2: "Daniel Bajenov" has added 20% of the code in this file
// Principal Author: Juan Pablo Ordonez Gomez
// Description: Application-level state container used by Blazor components to track current user session.

namespace Mealventory.Web.Services
{
    /// <summary>
    /// Simple application state service that holds current user information and notifies components of changes.
    /// </summary>
    public class AppState
    {
        /// <summary>
        /// Currently authenticated user's id, if any.
        /// </summary>
        public int? CurrentUserId { get; private set; }

        /// <summary>
        /// Currently authenticated user's username, if any.
        /// </summary>
        public string? CurrentUserName { get; private set; }

        /// <summary>
        /// Event triggered when state changes.
        /// </summary>
        public event Action? OnChange;

        /// <summary>
        /// Logs a user in within the application state.
        /// </summary>
        /// <param name="userId">User id.</param>
        /// <param name="username">User name.</param>
        public void LogIn(int userId, string username)
        {
            CurrentUserId = userId;
            CurrentUserName = username;
            NotifyStateChanged();
        }

        /// <summary>
        /// Restores a previously saved user into the app state.
        /// </summary>
        /// <param name="userId">User id.</param>
        /// <param name="username">User name.</param>
        public void RestoreUser(int? userId, string? username)
        {
            CurrentUserId = userId;
            CurrentUserName = username;
            NotifyStateChanged();
        }

        /// <summary>
        /// Logs the user out of the application state.
        /// </summary>
        public void LogOut()
        {
            CurrentUserId = null;
            CurrentUserName = null;
            NotifyStateChanged();
        }

        /// Method to notify listeners that state has changed.
        private void NotifyStateChanged() => OnChange?.Invoke();
    }
}
