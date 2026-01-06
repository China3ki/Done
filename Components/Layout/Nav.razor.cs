using Microsoft.AspNetCore.Components;

namespace Done.Components.Layout
{
    public partial class Nav
    {
        [Parameter]
        public EventCallback HandleMenu { get; set; }
        [Parameter]
        public bool ShowMenu { get; set; } = false;
        private bool _showAuthList = false;
        private bool _showAuth = false;
        private AuthOption _authOption;
        private bool _showLatest = false;
        private void HandleAuthOption() => _showAuthList = _showAuthList == false;
        private void CloseAuth() => _showAuth = false;
        private void ShowAuth(AuthOption authOption)
        {
            _authOption = authOption;
            _showAuth = _showAuth = true;
        }
        private void HandleLatest() => _showLatest = _showLatest == false;
    }
}
