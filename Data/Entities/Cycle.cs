using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities;

public partial class Cycle
{
    public int Id { get; set; }

    public string Description { get; set; } = null!;
    public string Title { get; set; } = null!;

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public int? CourseId { get; set; }

    public string? SourcePath { get; set; }
    
    public bool IsNotificationSent { get; set; }

    public virtual ICollection<Attendance> Attendances { get; set; } = new List<Attendance>();
   
    [ForeignKey("CourseId")]
    public virtual Course? Course { get; set; }

}
