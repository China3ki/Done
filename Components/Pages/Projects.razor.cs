using Done.Components.Layout;
using Done.Models;
using Done.Models.LocalModels;
using Done.Services;
using Done.Services.ProjectsServices;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
namespace Done.Components.Pages
{
    public partial class Projects : IDisposable
    {
        [CascadingParameter]
        private Task<AuthenticationState> AuthStateTask { get; set; } = default!;
        [Inject]
        public ProjectServiceLocal LocalService { get; set; } = default!;
        [Inject]
        public ProjectServiceDb DbService { get; set; } = default!;
        [Inject]
        public UpdateProjectService NewProjectService { get; set; } = default!;
        [Inject]
        public NotificationService NotificationService { get; set; } = default!;
        private List<DisplayProjectModel> _projects = [];
        private List<DisplayProjectModel> _projectsToDisplay = [];
        private bool _showInfo = false;
        private bool _showAddProjects = false;
        private string _searchValue = string.Empty;
        private int _pages;
        private int _currentPage = 1;
        private SortOptions _sortOption = SortOptions.Asc;
        protected override async Task OnParametersSetAsync()
        {
            NewProjectService.OnProjectsChanged += ChangeProjectState;
            await LoadData();
            await base.OnParametersSetAsync();
        }
        private async void ChangeProjectState() 
        {
            await LoadData();
            StateHasChanged();
        }
        private async Task PinProject(int projectId)
        {
            (bool isAuthenticated, int userId) = await CheckAuthentication();
            if (isAuthenticated) await DbService.PinProject(projectId, userId);
            else await LocalService.PinProject(projectId);
            await LoadData();
            StateHasChanged();
        }
        private async Task HandleSynchronize(bool synchronize)
        {
            var authenticationState = await AuthStateTask;
            int userId = Convert.ToInt32(authenticationState.User.FindFirst("Id")?.Value);
            _showInfo = false;
            await DbService.SynchronizeProjects(userId, synchronize);
            await LoadData();
            NewProjectService.NotifyProjectsChanged();
            StateHasChanged();
        }
        private async Task HandleAddProjects(bool update)
        {
            _showAddProjects = _showAddProjects == false;
            if (update) await LoadData();
        }
        /// <summary>
        /// Sorting method for projects
        /// </summary>
        /// <param name="sortType">Sort options. If not specified, projects are sorted by Lp</param>
        private void Pagination(SortType sortType = SortType.Pinned) 
        {
            var query = _projects.AsQueryable();
            List<DisplayProjectModel> projects = [];
            if (!string.IsNullOrEmpty(_searchValue)) query = query.Where(p => p.Name.ToLower().Contains(_searchValue));
            query = sortType switch
            {
                SortType.Name => _sortOption == SortOptions.Asc ? query.OrderBy(p => p.Name) : query.OrderByDescending(p => p.Name),
                SortType.Date => _sortOption == SortOptions.Asc ? query.OrderBy(p => p.CreatedDate) : query.OrderByDescending(p => p.CreatedDate),
                SortType.Lp => _sortOption == SortOptions.Asc ? query.OrderBy(p => p.Lp) : query.OrderByDescending(p => p.Lp),
                _ => _sortOption == SortOptions.Asc ? query.OrderByDescending(p => p.Pinned)  : query.OrderBy(p => p.Pinned),
            };
            _sortOption = _sortOption == SortOptions.Asc ? SortOptions.Desc : SortOptions.Asc;
            projects = query.ToList();
            SetPages(projects);
            _projectsToDisplay = projects.Skip((_currentPage - 1) * 10).Take(10).ToList();
        }
        private void ChangePage(int page)
        {
            _currentPage = page;
            Pagination();
        }
        private void SetPages(List<DisplayProjectModel> projects) => _pages = (int)Math.Ceiling(projects.Count / 10d);
        private async Task LoadData()
        {
            (bool isAuthenticated, int userId) = await CheckAuthentication();
            if (isAuthenticated)
            {
                List<DisplayProjectModel> localStorageProjects = await DbService.GetProjectsFromLocalStorage();
                if (localStorageProjects.Count != 0) _showInfo = true;
                _projects = await DbService.GetProjectsFromDb(userId);
            }
            else _projects = await LocalService.GetProjectsFromLocalStorage();
            Pagination();
        }
        private async Task<(bool, int)> CheckAuthentication()
        {
            var authenicationState = await AuthStateTask;
            var user = authenicationState.User;
            if (int.TryParse(user.FindFirstValue("Id"), out int userId)) return (user.Identity?.IsAuthenticated ?? false, userId);
            else return (false, 0);
        }
        public void Dispose()
        {
            NewProjectService.OnProjectsChanged -= ChangeProjectState;
        }
    }
}
