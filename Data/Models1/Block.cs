using System;
using System.Collections.Generic;

namespace Data.Models1;

public partial class Block
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int BlockedUserId { get; set; }

    public virtual User BlockedUser { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
