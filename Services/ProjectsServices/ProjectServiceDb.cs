using Done.Components.Pages;
using Done.Entities;
using Done.Etc.Interfaces;
using Done.Models.LocalModels;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace Done.Services.ProjectsServices
{
    public class ProjectServiceDb(ProtectedLocalStorage localstorage, IDbContextFactory<DoneContext> DoneContext) : ProjectService(localstorage)
    {
        private readonly IDbContextFactory<DoneContext> _dbFactory = DoneContext;
        public async Task AddProject(Project project)
        {
            using var ctx = await _dbFactory.CreateDbContextAsync();
            ctx.Add(project);
            try
            {
                await ctx.SaveChangesAsync();
            } catch(Exception ex)
            {
                Console.WriteLine($"Add project db - {ex.Message}");
            }
        }
        public async Task<List<DisplayProjectModel>> GetProjectsFromDb(int userId)
        {
            using var ctx = await _dbFactory.CreateDbContextAsync();
            List<Project> projects = await ctx.Projects.Where(p => p.ProjectUserId == userId).ToListAsync();
            List<DisplayProjectModel> projectsToDisplay = [];
            int i = 1;
            foreach(var project in projects)
            {
                DisplayProjectModel newDisplayProject = new()
                {
                    Lp = i++,
                    Id = project.ProjectId,
                    Name = project.ProjectName,
                    CreatedDate = project.ProjectCreatedDate,
                    Pinned = project.ProjectPinned
                };
                projectsToDisplay.Add(newDisplayProject);
            }
            return projectsToDisplay;
        }
        public async Task SynchronizeProjects(int userId, bool synchronize)
        {
            if(synchronize)
            {
                List<DisplayProjectModel> localProjects = await GetProjectsFromLocalStorage();
                List<Project> projects = [];
                foreach(var localProject in localProjects)
                {
                    Project project = new()
                    {
                        ProjectName = localProject.Name,
                        ProjectCreatedDate = localProject.CreatedDate,
                        ProjectPinned = localProject.Pinned,
                        ProjectUserId = userId
                    };
                    projects.Add(project);
                }
                using var ctx = await _dbFactory.CreateDbContextAsync();
                ctx.AddRange(projects);
                try
                {
                    await ctx.SaveChangesAsync();
                }
                catch(Exception ex)
                {
                    Console.WriteLine($"Synchronize projects - {ex.Message}");
                    return;
                }
            }
            await LocalStorage.DeleteAsync("projects");
        }
        public async Task PinProject(int projectId, int userId)
        {
            Console.WriteLine(projectId);
            using var ctx = await _dbFactory.CreateDbContextAsync();
            var project = await ctx.Projects.FirstOrDefaultAsync(p => p.ProjectId == projectId && p.ProjectUserId == userId);
            if (project is null) return;
            if (project.ProjectPinned) project.ProjectPinned = false;
            else project.ProjectPinned = true;
            try
            {
                await ctx.SaveChangesAsync();
            } 
            catch(Exception ex)
            {
                Console.WriteLine($"Pinned - {ex.Message}");
            }
        }
        public async Task<int> GetProjectsNumber(int userId)
        {
            using var ctx = await _dbFactory.CreateDbContextAsync();
            int number = ctx.Projects.Count(p => p.ProjectUserId == userId);
            return number;
        }
    }
}
