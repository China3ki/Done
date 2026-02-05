using System;
using System.Collections.Generic;

namespace Done.Entities;

public partial class User
{
    public int UserId { get; set; }

    public string UserName { get; set; } = null!;

    public string UserSurname { get; set; } = null!;

    public string UserEmail { get; set; } = null!;

    public string? UserAvatar { get; set; }

    public string UserPassword { get; set; } = null!;

    public virtual ICollection<Assigment> Assigments { get; set; } = new List<Assigment>();

    public virtual ICollection<GroupMessage> GroupMessages { get; set; } = new List<GroupMessage>();

    public virtual ICollection<Message> MessageMessageCreators { get; set; } = new List<Message>();

    public virtual ICollection<Message> MessageMessageRecipients { get; set; } = new List<Message>();

    public virtual ICollection<ProjectsUser> ProjectsUsers { get; set; } = new List<ProjectsUser>();
}
