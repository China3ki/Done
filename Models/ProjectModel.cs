using System.ComponentModel.DataAnnotations;

namespace Done.Models
{
    public class ProjectModel
    {
        [Required(ErrorMessage = "Project name cannot be empty!"), MaxLength(50, ErrorMessage = "Name length can have only 50 characters!")]
        public string Name { get; set; } = string.Empty;
    }
}
