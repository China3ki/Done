namespace Done.Models
{
    public class JobModel
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int StatusPriorityId { get; set; }
        public int StatusId { get; set; }
    }
}
