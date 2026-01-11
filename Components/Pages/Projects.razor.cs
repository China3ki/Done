using Done.Models.LocalModels;
using Done.Services.ProjectsServices;
using Microsoft.AspNetCore.Components;

namespace Done.Components.Pages
{
    public partial class Projects
    {
        [Inject]
        public ProjectServiceLocal ProjectService { get; set; } = default!;
        private List<ProjectModel> _projects = [];
        private bool _showNewProject = false;
        private async Task CloseNewProject(bool updateProjects)
        {
            _showNewProject = false;
            _projects = await ProjectService.GetProjectsFromLocalStorage();
        }
        private void ShowNewProject() => _showNewProject = true;
        protected override async Task OnInitializedAsync()
        {
            _projects = await ProjectService.GetProjectsFromLocalStorage();
            await base.OnInitializedAsync();
        }
    }
}
