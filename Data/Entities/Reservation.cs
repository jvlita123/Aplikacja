using System;
using System.Collections.Generic;

namespace Service.Entities;

public partial class Reservation
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public DateTime Start { get; set; }

    public DateTime End { get; set; }

    public string? Title { get; set; }

    public int ServiceId { get; set; }

    public string? PrimaryColor { get; set; }

    public string? SecondaryColor { get; set; }

    public int? StatusId { get; set; }

    public virtual Service Service { get; set; } = null!;

    public virtual Status? Status { get; set; }

    public virtual User User { get; set; } = null!;
}
