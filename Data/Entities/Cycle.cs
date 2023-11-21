namespace Data.Entities;

public partial class Cycle
{
    public int Id { get; set; }

    public string Description { get; set; } = null!;

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public DateTime? VacationStartDate { get; set; }

    public DateTime? VacationEndDate { get; set; }

    public virtual ICollection<Attendance> Attendances { get; set; } = new List<Attendance>();

    public virtual ICollection<CoursesPerCycle> CoursesPerCycles { get; set; } = new List<CoursesPerCycle>();

    public virtual ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();

    public virtual ICollection<Survey> Surveys { get; set; } = new List<Survey>();
}
