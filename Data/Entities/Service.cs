﻿namespace Data.Entities;

public partial class Service
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public TimeSpan? ServiceTime { get; set; }

    public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();

    public virtual ICollection<ReservationSlots> ReservationSlots { get; set; } = new HashSet<ReservationSlots>();

}
