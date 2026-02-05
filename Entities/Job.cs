using System;
using System.Collections.Generic;

namespace Done.Entities;

public partial class Job
{
    public int JobId { get; set; }

    public string JobName { get; set; } = null!;

    public string? JobDescription { get; set; }

    public DateOnly JobStartdate { get; set; }

    public DateOnly? JobEnddate { get; set; }

    public int JobStatusPriorityId { get; set; }

    public int JobStatusId { get; set; }

    public int JobProjectId { get; set; }

    public int JobPriorityId { get; set; }

    public virtual ICollection<Assigment> Assigments { get; set; } = new List<Assigment>();

    public virtual Priority JobPriority { get; set; } = null!;

    public virtual Project JobProject { get; set; } = null!;

    public virtual Status JobStatus { get; set; } = null!;
}
