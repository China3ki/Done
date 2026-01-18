using Done.Entities;
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
        [Inject]
        public ProjectServiceDb ProjectServiceDb { get; set; } = default!;
        [Inject]
        public NewProjectService NewProjectService { get; set; } = default!;
        private readonly ProjectModel _newProjectModel = new();
        public async Task Close() => await CloseNewProject.InvokeAsync(false);
        public async Task Submit()
        {
            var authenticationState= await AuthService.GetAuthenticationStateAsync();
            bool isAuthenticated = authenticationState.User.Identity?.IsAuthenticated ?? false;
            if (!isAuthenticated) await SubmitLocal();
            else await SubmitToDb(Convert.ToInt32(authenticationState.User.FindFirst("Id")?.Value));
        }
        private async Task SubmitToDb(int userId)
        {
            Project project = new()
            {
                ProjectName = _newProjectModel.Name,
                ProjectCreatedDate = DateOnly.FromDateTime(DateTime.Today),
                ProjectPinned = false,
                ProjectUserId = userId
            };
            await ProjectServiceDb.AddProject(project);
            NewProjectService.NotifyProjectsChanged();
            await CloseNewProject.InvokeAsync(true);
        }
        private async Task SubmitLocal()
        {
            DisplayProjectModel project = new()
            {
                Name = _newProjectModel.Name,
                CreatedDate = DateOnly.FromDateTime(DateTime.Today),
                Pinned = false
            };
           await ProjectServiceLocal.AddProject(project);
            NewProjectService.NotifyProjectsChanged();
            await CloseNewProject.InvokeAsync(true);
        }
    }
}
