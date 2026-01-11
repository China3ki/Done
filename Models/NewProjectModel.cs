using System.ComponentModel.DataAnnotations;

namespace Done.Models
{
    public class NewProjectModel
    {
        [Required(ErrorMessage = "Project name cannot be empty!")]
        public string Name { get; set; } = string.Empty;
        public DateOnly CreatedDate { get; set; }
    }
}
