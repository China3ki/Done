using Done.Entities;
using Done.Models;
using Done.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Done.Components.Layout
{
    public partial class Register
    {
        [Parameter]
        public EventCallback CloseForm { get; set; }
        [Inject]
        public IDbContextFactory<DoneContext> DbFactory { get; set; } = default!;
        [Inject]
        public AuthService AuthService { get; set; } = default!;
        private EditContext? _editContext;
        private ValidationMessageStore? _messageStore;
        private readonly RegisterModel _register = new();
        private bool _accountCreatedInfo = false;

        protected override void OnInitialized()
        {
            _editContext = new(_register);
            _messageStore = new(_editContext);
            _editContext?.OnFieldChanged += (s, e) =>
            {
                if (e.FieldIdentifier.FieldName == nameof(_register.Email)) _messageStore?.Clear(e.FieldIdentifier);
            };
            base.OnInitialized();
        }
        private async Task Submit()
        {
            using var ctx = await DbFactory.CreateDbContextAsync();
            var userExist = await ctx.Users.AnyAsync(u => u.UserEmail == _register.Email);
            if (userExist)
            {
                _messageStore?.Add(_editContext!.Field(nameof(_register.Email)), "Account already Exist!");
                _editContext?.NotifyValidationStateChanged();
                return;
            }
            User user = new()
            {
                UserName = _register.Name,
                UserSurname = _register.Surname,
                UserEmail = _register.Email,
                UserAdmin = false
            };

            PasswordHasher<User> hasher = new();
            user.UserPassword = hasher.HashPassword(user, _register.Password);

            ctx.Add<User>(user);
            try
            {
                await ctx.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Register error: {ex.Message}");
                _messageStore?.Add(_editContext!.Field(nameof(_register.Email)), "Database is unavailable, try again later!");
                _editContext?.NotifyValidationStateChanged();
                return;
            }

            _accountCreatedInfo = true;
            await Task.Delay(1000);
            await AuthService.CreateAuthenticationState(AuthService.CreateSessionModel(user));
            await CloseForm.InvokeAsync();
        }
    }
}
