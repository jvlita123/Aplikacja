namespace Data.Entities;

public partial class Course
{
    public int Id { get; set; }

    public string Description { get; set; } = null!;

    public string? Title { get; set; }

    public string? PhotoPath { get; set; } = null;

    public virtual ICollection<Attendance>? Attendances { get; set; }

    public virtual ICollection<Cycle>? Cycles { get; set; }

    public virtual ICollection<Enrollment>? Enrollments { get; set; }

    public virtual ICollection<Survey>? Surveys { get; set; }
}
