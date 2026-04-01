namespace Mealventory.Web.Services
{
    public class AppState
    {
        public int? CurrentUserId { get; private set; }
        public string? CurrentUserName { get; private set; }

        public event Action? OnChange;

        public void LogIn(int userId, string username)
        {
            CurrentUserId = userId;
            CurrentUserName = username;
            NotifyStateChanged();
        }

        public void LogOut()
        {
            CurrentUserId = null;
            CurrentUserName = null;
            NotifyStateChanged();
        }

        private void NotifyStateChanged() => OnChange?.Invoke();
    }
}