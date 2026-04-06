// Owner 1: "Juan Pablo Ordonez Gomez" has added 78.79% of the code in this file
namespace Mealventory.Web.Services
{
    /// Stores and broadcasts authenticated user state for the Blazor app.
    public class AppState
    {
        /// Field to store the currently logged in user identifier.
        public int? CurrentUserId { get; private set; }

        /// Field to store the currently logged in username.
        public string? CurrentUserName { get; private set; }

        /// Field to notify subscribers when state changes.
        public event Action? OnChange;

        /// Method to set current user state as logged in.
        public void LogIn(int userId, string username)
        {
            CurrentUserId = userId;
            CurrentUserName = username;
            NotifyStateChanged();
        }

        /// Method to restore user state from persisted values.
        public void RestoreUser(int? userId, string? username)
        {
            CurrentUserId = userId;
            CurrentUserName = username;
            NotifyStateChanged();
        }

        /// Method to clear current user state as logged out.
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