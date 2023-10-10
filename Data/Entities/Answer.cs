using System;
using System.Collections.Generic;

namespace Service.Entities;

public partial class Answer
{
    public int Id { get; set; }

    public int QuestionId { get; set; }

    public int? AnswerOptionId { get; set; }

    public virtual AnswerOption? AnswerOption { get; set; }

    public virtual Question Question { get; set; } = null!;
}
