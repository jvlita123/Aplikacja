using System;
using System.Collections.Generic;

namespace Service.Entities;

public partial class Response
{
    public int Id { get; set; }

    public string? ResultSurvey { get; set; }

    public int UserId { get; set; }

    public int SurveyId { get; set; }

    public virtual Survey Survey { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
