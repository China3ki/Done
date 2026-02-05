using System;
using System.Collections.Generic;

namespace Done.Entities;

public partial class GroupChat
{
    public int ChatId { get; set; }

    public int ChatProjectId { get; set; }

    public virtual Project ChatProject { get; set; } = null!;

    public virtual ICollection<GroupMessage> GroupMessages { get; set; } = new List<GroupMessage>();
}
