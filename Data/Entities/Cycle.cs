using System;
using System.Collections.Generic;

namespace Data.Entities;

public partial class Cycle
{
    public int Id { get; set; }

    public string Description { get; set; } = null!;

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public int? CourseId { get; set; }

    public virtual ICollection<Attendance> Attendances { get; set; } = new List<Attendance>();

    public virtual Course? Course { get; set; }

    public virtual ICollection<Survey> Surveys { get; set; } = new List<Survey>();
}
