using System.ComponentModel.DataAnnotations;

namespace Done.Models
{
    public class NewProjectModel
    {
        [Required(ErrorMessage = "Project name cannot be empty!")]
        public string ProjectName { get; set; } = string.Empty;
    }
}
