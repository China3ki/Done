
namespace Done.Models.LocalModels
{
    public class DisplayProjectModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateOnly CreatedDate { get; set; }
        public bool Pinned { get; set; }
    }
}
