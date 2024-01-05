namespace Data.Entities;

public partial class AnswerOption
{
    public int Id { get; set; }

    public string AnswerText { get; set; } = null!;

    public int QuestionId { get; set; }

    public virtual ICollection<Answer> Answers { get; set; } = new List<Answer>();

    public virtual Question Question { get; set; } = null!;
}
