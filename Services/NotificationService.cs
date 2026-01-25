using Done.Components.Layout;
using Done.Models;

namespace Done.Services
{
    public class NotificationService
    {
        public List<NotificationModel> _notifications = [];
        public Action? OnChange { get; set; }
        public async Task ShowNotification(NotificationModel notification)
        {
            _notifications.Add(notification);
            NotifyStateChange();
            RemoveAfterDelay(notification);
        }
        public void Remove(NotificationModel notification)
        {
            if (_notifications.Contains(notification)) _notifications.Remove(notification);
            NotifyStateChange();
        }
        private async void RemoveAfterDelay(NotificationModel notification)
        {
            await Task.Delay(3000);
            Remove(notification);
            NotifyStateChange();
        }
        public void NotifyStateChange() => OnChange?.Invoke();
    }
}
