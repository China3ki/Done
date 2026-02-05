using System.ComponentModel.DataAnnotations;

namespace Done.Models
{
    public class JobModel
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;
        [Required]
        public int Priority { get; set; }
        public DateOnly CreatedDate { get; set; } = DateOnly.FromDateTime(DateTime.Now);
        public DateOnly? EndDate { get; set; }
    }
}
