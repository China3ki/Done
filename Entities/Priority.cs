using System;
using System.Collections.Generic;

namespace Done.Entities;

public partial class Priority
{
    public int PriorityId { get; set; }

    public string PriorityName { get; set; } = null!;

    public virtual ICollection<Job> Jobs { get; set; } = new List<Job>();
}
