using System;
using System.Collections.Generic;

namespace Data.Entities;

public partial class Photo
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public string? Description { get; set; }

    public string? Path { get; set; }

    public bool? IsProfilePicture { get; set; }

    public DateTime? Date { get; set; }

    public virtual User User { get; set; } = null!;
}
