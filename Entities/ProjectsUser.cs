using System;
using System.Collections.Generic;

namespace Done.Entities;

public partial class ProjectsUser
{
    public int ProjectUserId { get; set; }

    public int ProjectId { get; set; }

    public int UserId { get; set; }

    public bool ProjectAdmin { get; set; }

    public virtual Project Project { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
