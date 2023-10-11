using System;
using System.Collections.Generic;

namespace Data.Entities;

public partial class Attendance
{
    public int Id { get; set; }

    public int CourseId { get; set; }

    public int CycleId { get; set; }

    public int UserId { get; set; }

    public TimeSpan? TimeArrive { get; set; }

    public TimeSpan? TimeLeave { get; set; }

    public virtual Course Course { get; set; } = null!;

    public virtual Cycle Cycle { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
