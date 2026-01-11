namespace Done.Components.Pages
{
    public partial class Projects
    {
        private bool _showNewProject = false;
        private void CloseNewProject() => _showNewProject = false;
        private void ShowNewProject() => _showNewProject = true;
    }
}
