using Done.Etc.Interfaces;
using Done.Models.LocalModels;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using System.Threading.Tasks;

namespace Done.Services.ProjectsServices
{
    public class ProjectServiceLocal(ProtectedLocalStorage localStorage) : ProjectService(localStorage)
    {
        public async Task AddProject(DisplayProjectModel project)
        {
            List<DisplayProjectModel> projects = await GetProjectsFromLocalStorage();
            if (projects.Count != 0) project.Id = projects.Last().Id + 1;
            else project.Id = 1;
            projects.Add(project);
            await LocalStorage.SetAsync("projects", projects);
        }
        public async Task DeleteProject()
        {

        }
        public async Task EditProject()
        {
            
        }
        public async Task PinProject(int projectId)
        {
            List<DisplayProjectModel> projects = await GetProjectsFromLocalStorage();
            DisplayProjectModel? project = projects.FirstOrDefault(p => p.Id == projectId);
            if (project is not null)
            {
                if (project.Pinned) project.Pinned = false;
                else project.Pinned = true;
                await LocalStorage.SetAsync("projects", projects);
            }
        }
        public async Task<int> GetProjectsNumber()
        {
            List<DisplayProjectModel> projects = await GetProjectsFromLocalStorage();
            return projects.Count;
        }
    }
}
