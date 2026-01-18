using System;
using System.Collections.Generic;

namespace Done.Entities;

public partial class Project
{
    public int ProjectId { get; set; }

    public string ProjectName { get; set; } = null!;

    public DateOnly ProjectCreatedDate { get; set; }

    public bool ProjectPinned { get; set; }

    public int ProjectUserId { get; set; }

    public virtual User ProjectUser { get; set; } = null!;
}
