using Microsoft.AspNetCore.Components;

namespace Done.Components.Layout
{
    public partial class Notification
    {
        [Parameter]
        public EventCallback OnConfirm { get; set; }
        [Parameter]
        public EventCallback OnCancel { get; set; }
        [Parameter]
        public string Info { get; set; } = string.Empty;
        [Parameter]
        public string Question { get; set; } = string.Empty;
    }
}
