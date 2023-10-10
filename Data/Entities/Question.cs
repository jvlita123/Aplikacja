using System;
using System.Collections.Generic;

namespace Service.Entities;

public partial class Question
{
    public int Id { get; set; }

    public int SurveyId { get; set; }

    public string QuestionText { get; set; } = null!;

    public virtual ICollection<AnswerOption> AnswerOptions { get; set; } = new List<AnswerOption>();

    public virtual ICollection<Answer> Answers { get; set; } = new List<Answer>();

    public virtual Survey Survey { get; set; } = null!;
}
