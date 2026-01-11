namespace Done.Models.LocalModels
{
    public class ProjectModel
    {
        public NewProjectModel Project { get; set; } = default!;
        public List<JobModel> Jobs { get; set; } = [];
    }
}
