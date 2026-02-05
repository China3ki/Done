namespace Done.Models.DisplayModels
{
    public class DisplayJobModel
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;
        public string Priority { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public DateOnly CreatedDate { get; set; }
        public DateOnly? EndDate { get; set; }
    }
}
