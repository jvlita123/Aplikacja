using System;
using System.Collections.Generic;

namespace Service.Entities;

public partial class CoursesPerCycle
{
    public int Id { get; set; }

    public int CourseId { get; set; }

    public int CycleId { get; set; }

    public virtual Course Course { get; set; } = null!;

    public virtual Cycle Cycle { get; set; } = null!;
}
