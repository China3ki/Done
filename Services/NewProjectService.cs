namespace Done.Services
{
    public class NewProjectService
    {
        public event Action? OnProjectsChanged;
        public void NotifyProjectsChanged() => OnProjectsChanged?.Invoke();
    }
}
