using System;
using System.Collections.Generic;

namespace Data.Entities;

public partial class Service
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public TimeSpan? ServiceTime { get; set; }

    public virtual ICollection<Reservation1> Reservations { get; set; } = new List<Reservation1>();
}
