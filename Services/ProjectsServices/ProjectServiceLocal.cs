using Done.Models.LocalModels;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace Done.Services.ProjectsServices
{
    public class ProjectServiceLocal(ProtectedLocalStorage localStorage) : ProjectService(localStorage)
    {
        public async Task AddProject(ProjectModel project)
        {
            List<ProjectModel> projects = await GetProjectsFromLocalStorage();
            projects.Add(project);
            await LocalStorage.SetAsync("projects", projects);
        }
        public async Task DeleteProject()
        {

        }
        public async Task EditProject()
        {
            
        }
    }
}
