using System;
using System.Collections.Generic;

namespace Done.Entities;

public partial class Project
{
    public int ProjectId { get; set; }

    public string ProjectName { get; set; } = null!;

    public DateOnly ProjectCreatedDate { get; set; }

    public bool ProjectPinned { get; set; }

    public virtual ICollection<GroupChat> GroupChats { get; set; } = new List<GroupChat>();

    public virtual ICollection<Job> Jobs { get; set; } = new List<Job>();

    public virtual ICollection<ProjectsUser> ProjectsUsers { get; set; } = new List<ProjectsUser>();
}
