using Done.Models.LocalModels;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace Done.Services.ProjectsServices
{
    abstract public class ProjectService(ProtectedLocalStorage localstorage)
    {
        public ProtectedLocalStorage LocalStorage { get; set; } = localstorage;
        public async Task<List<DisplayProjectModel>> GetProjectsFromLocalStorage()
        {
            try
            {
                var results = await LocalStorage.GetAsync<List<DisplayProjectModel>>("projects");
                var data = results.Success && results.Value != null ? results.Value : new List<DisplayProjectModel>();
                return data;
            } catch(Exception ex)
            {
                Console.WriteLine($"Project services - {ex.Message}");
            }
            return [];
        }
    }
}
