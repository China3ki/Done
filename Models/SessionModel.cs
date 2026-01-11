namespace Done.Models
{
    public class SessionModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        public string? Avatar { get; set; } = string.Empty;
        public bool Admin { get; set; }
        public DateTime ExpiresTime { get; set; }
    }
}
