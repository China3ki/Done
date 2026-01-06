using Done.Models;
using Microsoft.AspNetCore.Components;

namespace Done.Components.Layout
{
    public partial class Login
    {
        [Parameter]
        public EventCallback CloseForm { get; set; }
        private LoginModel _login = new();
    }
}
