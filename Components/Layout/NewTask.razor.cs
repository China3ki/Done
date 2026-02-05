using Done.Entities;
using Done.Models;
using Done.Services;
using Done.Services.JobsServices;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Done.Components.Layout
{
    public partial class NewTask
    {
        [CascadingParameter]
        public Task<AuthenticationState> AuthStateTask { get; set; } = default!;
        [Parameter]
        public int ProjectId { get; set; }
        [Inject]
        public JobServiceDb JobServiceDb { get; set; } = default!;
        [Inject]
        public NotificationService NotificationService { get; set; } = default!;
        private JobModel _newJobModel = new();
        private EditContext? _editContext;
        private ValidationMessageStore? _messageStore;

        

        protected override void OnInitialized()
        {
            _editContext = new(_newJobModel);
            _messageStore = new(_editContext);
            _editContext.OnFieldChanged += (s, e) =>
            {
                if (e.FieldIdentifier.FieldName == nameof(_newJobModel.EndDate)) _messageStore.Clear(e.FieldIdentifier);
            };
            base.OnInitialized();
        }
        private async Task Submit()
        {
            (bool isAuthenticated, int userId) = await CheckAunthentication();
            NotificationModel notificationModel = new();
            if (!isAuthenticated)
            {
                notificationModel.Info = "You do not have permission to do this!";
                notificationModel.InfoType = InfoType.Error;
                return;
            }


            if (_newJobModel.CreatedDate > _newJobModel.EndDate)
            {
                _messageStore?.Add(_editContext!.Field(nameof(_newJobModel.EndDate)), "Creation date must be before the end date!");
                _editContext?.NotifyValidationStateChanged();
                return;
            }
            Job job = new()
            {
                JobName = _newJobModel.Name,
                JobDescription = _newJobModel.Description,
                JobStartdate = _newJobModel.CreatedDate,
                JobEnddate = _newJobModel.EndDate,
                JobProjectId = ProjectId,
                JobPriorityId = _newJobModel.Priority
            };
            (bool success, string message) = await JobServiceDb.AddJob(job);
            notificationModel.Info = message;
            if (success) notificationModel.InfoType = InfoType.Success;
            else notificationModel.InfoType = InfoType.Error;
            await NotificationService.ShowNotification(notificationModel);
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
