using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities;

public partial class Message
{
    public int Id { get; set; }

    public string? Text { get; set; }

    public int UserId { get; set; }

    public int UserId2 { get; set; }

    [NotMapped]
    public virtual User User { get; set; } = null!;

    public virtual User UserId2Navigation { get; set; } = null!;
}
