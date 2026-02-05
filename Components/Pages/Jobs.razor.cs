
using Done.Models.DisplayModels;
using Done.Services.JobsServices;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Done.Components.Pages
{
    public partial class Jobs
    {
        [CascadingParameter]
        public Task<AuthenticationState> AuthStateTask { get; set; } = default!;
        [Inject]
        public JobServiceDb JobServiceDb { get; set; } = default!;
        [Inject]
        public NavigationManager NavigationManager { get; set; } = default!;
        [Parameter]
        public int ProjectId { get; set; }
        private List<DisplayJobModel> _jobsModel = [];
        private List<DisplayJobModel> _jobsToDisplay = [];
 
        private async Task LoadData()
        {
            (bool isAuthenticated, int userId) = await CheckAunthentication();
            if(isAuthenticated)
            {
                (List<DisplayJobModel> jobs, OperationResult operationResult) = await JobServiceDb.GetJobsFromDb(userId, ProjectId);
                if (operationResult == OperationResult.Success) _jobsModel = jobs;
            } else
            {
                NavigationManager.NavigateTo("/");
            }
        }
        private void Pagination()
        {

        }
        private async Task<(bool, int)> CheckAunthentication()
        {
            var authenticationState = await AuthStateTask;
            var user = authenticationState.User;
            if (int.TryParse(user.FindFirstValue("Id"), out int userId)) return (true, userId);
            else return (false, 0);
        }
    }
}
