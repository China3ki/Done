using System;
using System.Collections.Generic;

namespace Done.Entities;

public partial class GroupMessage
{
    public int MessageId { get; set; }

    public string MessageContent { get; set; } = null!;

    public int MessageUserId { get; set; }

    public int MessageChatId { get; set; }

    public virtual GroupChat MessageChat { get; set; } = null!;

    public virtual User MessageUser { get; set; } = null!;
}
