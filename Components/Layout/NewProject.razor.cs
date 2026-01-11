using Done.Models;
using Done.Models.LocalModels;
using Done.Services;
using Done.Services.ProjectsServices;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace Done.Components.Layout
{
    public partial class NewProject
    {
        [Parameter]
        public EventCallback<bool> CloseNewProject { get; set; }
        [Inject]
        public AuthService AuthService { get; set; } = default!;
        [Inject]
        public ProjectServiceLocal ProjectServiceLocal { get; set; } = default!;
        private readonly NewProjectModel _newProjectModel = new();
        public async Task Close() => await CloseNewProject.InvokeAsync(false);
        public async Task Submit()
        {
            var authenticationState= await AuthService.GetAuthenticationStateAsync();
            bool isAuthenticated = authenticationState.User.Identity?.IsAuthenticated ?? false;
            if (!isAuthenticated) await SubmitLocal();
            else await SubmitToDb();
        }
        private async Task SubmitToDb()
        {

        }
        private async Task SubmitLocal()
        {
            _newProjectModel.CreatedDate = DateOnly.FromDateTime(DateTime.Today);
            ProjectModel project = new()
            {
                Project = _newProjectModel
            };
           await ProjectServiceLocal.AddProject(project);
           await CloseNewProject.InvokeAsync(true);
        }
    }
}
