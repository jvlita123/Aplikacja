using Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities;

public partial class ReservationSlots
{
    public int Id { get; set; }

    public int ServiceId { get; set; }
    public int? ReservationId { get; set; }

    public DateTime Date { get; set; }

    public TimeSpan StartTime { get; set; }

    public TimeSpan EndTime { get; set; }

    public bool IsAvailable { get; set; }

    public virtual Service Service { get; set; } = null!;

    [ForeignKey("ReservationId")]
    public virtual Reservation1? Reservation { get; set; }

}
