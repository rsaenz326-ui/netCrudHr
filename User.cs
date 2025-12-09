using System;
using System.Collections.Generic;

namespace WebAppHr4;

public partial class User
{
    public int UserId { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public int EmployeeId { get; set; }

    public int? StatusId { get; set; }

    public virtual Employee Employee { get; set; } = null!;

    public virtual UserStatus? Status { get; set; }

    public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
}
