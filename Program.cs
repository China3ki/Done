using Done.Components;
using Done.Components.Layout;
using Done.Entities;
using Done.Services;
using Done.Services.ProjectsServices;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();


// Scopes
builder.Services.AddDbContextFactory<DoneContext>(opt =>
{
    opt.UseNpgsql(builder.Configuration["ConnectionString"]);
});
builder.Services.AddScoped<ProtectedLocalStorage>();
builder.Services.AddScoped<ProtectedSessionStorage>();
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<AuthenticationStateProvider>(provider => provider.GetRequiredService<AuthService>());
builder.Services.AddScoped<ProjectServiceLocal>();
builder.Services.AddScoped<ProjectServiceDb>();
builder.Services.AddScoped<NewProjectService>();
builder.Services.AddAuthenticationCore();
builder.Services.AddCascadingAuthenticationState();


var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
