using Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities;

public partial class Block
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int BlockedUserId { get; set; }

    [NotMapped]
    public virtual User BlockedUser { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
