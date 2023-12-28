using System;
using System.Collections.Generic;

namespace Data.Models;

public partial class Survey
{
    public int Id { get; set; }

    public int CourseId { get; set; }

    public int CycleId { get; set; }

    public int UserId { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public virtual Course Course { get; set; } = null!;

    public virtual Cycle Cycle { get; set; } = null!;

    public virtual ICollection<Question> Questions { get; set; } = new List<Question>();

    public virtual ICollection<Response> Responses { get; set; } = new List<Response>();

    public virtual User User { get; set; } = null!;
}
