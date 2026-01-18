using Done.Services;
using Done.Services.ProjectsServices;
using Microsoft.AspNetCore.Components;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Done.Components.Layout
{
    public partial class Nav : IDisposable
    {
        [Parameter]
        public EventCallback HandleMenu { get; set; }
        [Parameter]
        public bool ShowMenu { get; set; } = false;
        [Inject]
        public AuthService AuthService { get; set; } = default!;
        [Inject]
        public ProjectServiceDb DbService { get; set; } = default!;
        [Inject]
        public ProjectServiceLocal LocalService { get; set; } = default!;
        [Inject]
        public NewProjectService NewProjectService { get; set; } = default!;
        private int _projectsNumber = 0;
        private bool _showAuthList = false;
        private bool _showAuth = false;
        private AuthOption _authOption;
        private bool _showLatest = false;
        private bool _showNewProject = false;
        private void ShowNewProject() => _showNewProject = true;
        private void CloseNewProject() => _showNewProject = false;
        private void HandleAuthOption() => _showAuthList = _showAuthList == false;
        private void CloseAuth() => _showAuth = false;
        protected override async Task OnInitializedAsync()
        {
            NewProjectService.OnProjectsChanged += GetProjectsNumber;
            GetProjectsNumber();
            await base.OnInitializedAsync();
        }
        private void ShowAuth(AuthOption authOption)
        {
            _authOption = authOption;
            _showAuth = true;
            _showAuthList = false;
        }
        private async Task<(bool, int)> CheckAuthentication()
        {
            var authenicationState = await AuthService.GetAuthenticationStateAsync();
            var user = authenicationState.User;
            if (int.TryParse(user.FindFirstValue("Id"), out int userId)) return (user.Identity?.IsAuthenticated ?? false, userId);
            else return (false, 0);
        }
        private async void GetProjectsNumber()
        {
            (bool isAunthenticated, int userId) = await CheckAuthentication();
            if (isAunthenticated) _projectsNumber = await DbService.GetProjectsNumber(userId);
            else _projectsNumber = await LocalService.GetProjectsNumber();
            StateHasChanged();
        }
        private async Task Logout() => await AuthService.DestroyAuthenticationState();
        private void HandleLatest() => _showLatest = _showLatest == false;
        public void Dispose()
        {
            NewProjectService.OnProjectsChanged -= GetProjectsNumber;
        }
    }
}
