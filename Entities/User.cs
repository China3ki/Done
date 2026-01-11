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

    public bool UserAdmin { get; set; }
}
