sing System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities;

public partial class Survey
{
    public int Id { get; set; }

    public int CourseId { get; set; }

    public int CycleId { get; set; }

    public int UserId { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    [ForeignKey("CourseId")]
    public virtual Course Course { get; set; } = null!;

    [ForeignKey("CycleId")]
    public virtual Cycle Cycle { get; set; } = null!;

    [ForeignKey("UserId")]
    public virtual User User { get; set; } = null!;

    public virtual ICollection<Question> Questions { get; set; } = new List<Question>();

    public virtual ICollection<Response> Responses { get; set; } = new List<Response>();
}
