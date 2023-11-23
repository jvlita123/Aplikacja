using System;
using System.Collections.Generic;

namespace Data.Entities;

public partial class Enrollment
{
    public int Id { get; set; }

    public int CourseId { get; set; }

    public int UserId { get; set; }

    public DateTime EnrollmentDate { get; set; }

    public bool Cancelled { get; set; }

    public string? CancellationReason { get; set; }

    public virtual Course Course { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
