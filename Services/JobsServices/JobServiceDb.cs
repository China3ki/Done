using Done.Components.Pages;
using Done.Entities;
using Done.Models.DisplayModels;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;

namespace Done.Services.JobsServices
{
    public class JobServiceDb(IDbContextFactory<DoneContext> dbFactory)
    {
        private IDbContextFactory<DoneContext> _dbFactory = dbFactory;
        public async Task<(List<DisplayJobModel>, OperationResult)> GetJobsFromDb(int userId, int projectId)
        {
            using var ctx = _dbFactory.CreateDbContext();
            try
            {
                bool projectAuthorization = await ctx.ProjectsUsers.AnyAsync(p => p.UserId == userId && p.ProjectId == projectId);
                if (!projectAuthorization) return ([], OperationResult.Unauthorized);

                var jobs = await ctx.Jobs.Where(j => j.JobProjectId == projectId && j.JobProject.ProjectsUsers.Any(p => p.ProjectUserId == userId)).ToListAsync();
                List<DisplayJobModel> jobsModel = [];
                foreach(var job in jobs)
                {
                    DisplayJobModel jobModel = new()
                    {
                        Name = job.JobName,
                        Description = job.JobDescription,
                        Priority = job.JobPriority.PriorityName,
                        Status = job.JobStatus.StatusName,
                        CreatedDate = job.JobStartdate,
                        EndDate = job.JobEnddate
                    };
                    jobsModel.Add(jobModel);
                }
                return (jobsModel, OperationResult.Success);
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Job service - ${ex.Message}");
                return ([], OperationResult.Error);
            }
        }
        public async Task<(bool, string)> AddJob(Job job)
        {
            using var ctx = await _dbFactory.CreateDbContextAsync();
            ctx.Add(job);
            try
            {
                await ctx.SaveChangesAsync();
                return (true, "Task has been added!");
            } catch(Exception ex)
            {
                Console.WriteLine($"Add job - {ex.Message}");
                return (false, "Something goes wrong, try again later!");
            }
        }
    } 
}
