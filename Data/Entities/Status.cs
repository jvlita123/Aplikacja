using System;
using System.Collections.Generic;

namespace Data.Entities;

public partial class Status
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Reservation1> Reservations { get; set; } = new List<Reservation1>();
}
