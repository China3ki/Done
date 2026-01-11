using Done.Models.LocalModels;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace Done.Services.ProjectsServices
{
    abstract public class ProjectService(ProtectedLocalStorage localstorage)
    {
        public ProtectedLocalStorage LocalStorage { get; set; } = localstorage;
        public async Task<List<ProjectModel>> GetProjectsFromLocalStorage()
        {
            try
            {
                var results = await LocalStorage.GetAsync<List<ProjectModel>>("projects");
                var data = results.Success && results.Value != null ? results.Value : new List<ProjectModel>();
                return data;
            } catch(Exception ex)
            {
                Console.WriteLine($"Project services - {ex.Message}");
            }
            return [];
        }
    }
}
