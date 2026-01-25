using Done.Models;
using Done.Services;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace Done.Components.Layout
{
    public partial class NotificationSmall : IDisposable
    {
        [Inject]
        public NotificationService NotificationService { get; set; } = default!;
        private void Remove(NotificationModel notificationModel) => NotificationService.Remove(notificationModel);
        protected override void OnInitialized()
        {
            NotificationService.OnChange += StateHasChanged;
            base.OnInitialized();
        }
       public void Dispose()
        {
            NotificationService.OnChange -= StateHasChanged;
        }
    }
}
