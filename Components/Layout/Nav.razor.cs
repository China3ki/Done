using Done.Services;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace Done.Components.Layout
{
    public partial class Nav
    {
        [Parameter]
        public EventCallback HandleMenu { get; set; }
        [Parameter]
        public bool ShowMenu { get; set; } = false;
        [Inject]
        public AuthService AuthService { get; set; } = default!;
        private bool _showAuthList = false;
        private bool _showAuth = false;
        private AuthOption _authOption;
        private bool _showLatest = false;
        private bool _showNewProject = false;
        private void ShowNewProject() => _showNewProject = true;
        private void CloseNewProject() => _showNewProject = false;
        private void HandleAuthOption() => _showAuthList = _showAuthList == false;
        private void CloseAuth() => _showAuth = false;
        private void ShowAuth(AuthOption authOption)
        {
            _authOption = authOption;
            _showAuth = true;
            _showAuthList = false;
        }
        private async Task Logout() => await AuthService.DestroyAuthenticationState();
        private void HandleLatest() => _showLatest = _showLatest == false;
    }
}
