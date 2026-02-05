using System;
using System.Collections.Generic;

namespace Done.Entities;

public partial class Message
{
    public int MessageId { get; set; }

    public string MessageContent { get; set; } = null!;

    public int MessageCreatorId { get; set; }

    public int MessageRecipientId { get; set; }

    public virtual User MessageCreator { get; set; } = null!;

    public virtual User MessageRecipient { get; set; } = null!;
}
