using System;
using System.Collections.Generic;

namespace Done.Entities;

public partial class Assigment
{
    public int AssigmentId { get; set; }

    public int AssigmentJobId { get; set; }

    public int AssigmentUserId { get; set; }

    public virtual Job AssigmentJob { get; set; } = null!;

    public virtual User AssigmentUser { get; set; } = null!;
}
