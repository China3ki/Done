using Done.Entities;
using Done.Models;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using System.Security.Claims;

namespace Done.Services
{
    public class AuthService(ProtectedSessionStorage sessionStorage) : AuthenticationStateProvider
    {
        private readonly ProtectedSessionStorage _sessionStorage = sessionStorage;
        private readonly ClaimsPrincipal _anonymous = new(new ClaimsIdentity());
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            try
            {
                var sessionExist = await _sessionStorage.GetAsync<SessionModel>("userSession");
                var sessionResult = sessionExist.Success ? sessionExist.Value : null;

                if (sessionResult is null) return new AuthenticationState(_anonymous);

                if (sessionResult.ExpiresTime < DateTime.UtcNow) return new AuthenticationState(_anonymous);

                sessionResult.ExpiresTime = DateTime.UtcNow.AddMinutes(30);
                await _sessionStorage.SetAsync("userSession", sessionResult);

                return new AuthenticationState(CreateSessionClaims(sessionResult));
            } catch
            {
                return new AuthenticationState(_anonymous);
            }
        }
        public async Task CreateAuthenticationState(SessionModel sessionModel)
        {
            sessionModel.ExpiresTime = DateTime.UtcNow.AddMinutes(30);
            await _sessionStorage.SetAsync("userSession", sessionModel);
            ClaimsPrincipal claims = CreateSessionClaims(sessionModel);
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claims)));
        }
        public async Task DestroyAuthenticationState()
        {
            await _sessionStorage.DeleteAsync("userSession");
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(_anonymous)));
        }
        public SessionModel CreateSessionModel(User user)
        {
            SessionModel sessionModel = new()
            {
                Id = user.UserId,
                Name = user.UserName,
                Surname = user.UserSurname,
                Avatar = user.UserAvatar ?? null,
            };
            return sessionModel;
        }
        private ClaimsPrincipal CreateSessionClaims(SessionModel sessionModel)
        {
            ClaimsPrincipal claims = new(new ClaimsIdentity(new[]
            {
                new Claim("Id", sessionModel.Id.ToString(), ClaimValueTypes.Integer32),
                new Claim(ClaimTypes.Name, sessionModel.Name),
                new Claim(ClaimTypes.Surname, sessionModel.Surname),
                new Claim("Avatar", sessionModel.Avatar ?? string.Empty),
                new Claim("Admin", sessionModel.Admin.ToString(), ClaimValueTypes.Boolean)
            }, "Auth"));
            return claims;
        }
    }
}
