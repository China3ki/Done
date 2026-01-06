using Done.Models;
using Microsoft.AspNetCore.Components;

namespace Done.Components.Layout
{
    public partial class Register
    {
        [Parameter]
        public EventCallback CloseForm { get; set; }
        private RegisterModel _register = new();
    }
}
