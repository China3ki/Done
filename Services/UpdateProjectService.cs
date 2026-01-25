namespace Done.Services
{
    public class UpdateProjectService
    {
        public event Action? OnProjectsChanged;
        public void NotifyProjectsChanged() => OnProjectsChanged?.Invoke();
    }
}
