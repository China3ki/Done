using Done.Entities;
using Done.Models;
using Done.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace Done.Components.Layout
{
    public partial class Login
    {
        [Parameter]
        public EventCallback CloseForm { get; set; }
        [Inject]
        public IDbContextFactory<DoneContext> DbFactory { get; set; } = default!;
        [Inject]
        public AuthService AuthService { get; set; } = default!;
        [Inject]
        public UpdateProjectService UpdateProjectService { get; set; } = default!;
        [Inject]
        public NotificationService NotificationService { get; set; } = default!;
        private EditContext? _editContext;
        private ValidationMessageStore? _messageStore;
        private readonly LoginModel _login = new();
        protected override void OnInitialized()
        {
            _editContext = new(_login);
            _messageStore = new(_editContext);
            _editContext.OnFieldChanged += (s, e) =>
            {
                if (e.FieldIdentifier.FieldName == nameof(_login.Email) || e.FieldIdentifier.FieldName == nameof(_login.Password)) _messageStore?.Clear();
            };
            base.OnInitialized();
        }
        private async Task Submit()
        {
            using var ctx = await DbFactory.CreateDbContextAsync();
            var user = await ctx.Users.FirstOrDefaultAsync(u => u.UserEmail == _login.Email);
            if (user is null)
            {
                FailedLoginMessage();
                return;
            }

            PasswordHasher<User> hasher = new();
            var verifyPassword = hasher.VerifyHashedPassword(user, user.UserPassword, _login.Password);

            if(verifyPassword == PasswordVerificationResult.Failed)
            {
                FailedLoginMessage();
                return;
            }

            await AuthService.CreateAuthenticationState(AuthService.CreateSessionModel(user));
            await CloseForm.InvokeAsync();
            UpdateProjectService.NotifyProjectsChanged();
            await NotificationService.ShowNotification(new NotificationModel() { Info = $"Hello {user.UserName}!", InfoType = InfoType.Success });
        }
        private void FailedLoginMessage()
        {
                _messageStore?.Add(_editContext!.Field(nameof(_login.Email)), "Account does not exist or password is wrong!");
                _editContext?.NotifyValidationStateChanged();
        }
    }
}
