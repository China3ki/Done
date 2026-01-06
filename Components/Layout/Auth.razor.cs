using Microsoft.AspNetCore.Components;

namespace Done.Components.Layout
{
    public partial class Auth
    {
        [Parameter]
        public EventCallback CloseForm { get; set; }
        [Parameter]
        public AuthOption AuthOption { get; set; }
        private void HandleAuthOption(AuthOption authOption) => AuthOption = authOption;
    }
}
