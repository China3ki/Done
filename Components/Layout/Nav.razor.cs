using Microsoft.AspNetCore.Components;

namespace Done.Components.Layout
{
    public partial class Nav
    {
        [Parameter]
        public EventCallback HandleMenu { get; set; }
        [Parameter]
        public bool ShowMenu { get; set; } = false;
        private bool _showAuthOption = false;
        private bool _showLatest = false;
        private void HandleAuthOption() => _showAuthOption = _showAuthOption == false;
        private void HandleLatest() => _showLatest = _showLatest == false;
    }
}
