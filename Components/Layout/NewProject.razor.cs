using Done.Models;
using Microsoft.AspNetCore.Components;

namespace Done.Components.Layout
{
    public partial class NewProject
    {
        [Parameter]
        public EventCallback CloseNewProject { get; set; }
        private readonly NewProjectModel _newProjectModel = new();
        public void Submit()
        {

        }
    }
}
