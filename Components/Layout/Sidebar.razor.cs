using Microsoft.AspNetCore.Components;

namespace Done.Components.Layout
{
    public partial class Sidebar
    {
        [Parameter]
        public EventCallback ShowMenu { get; set; }
    }
}
