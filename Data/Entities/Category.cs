using System;
using System.Collections.Generic;

namespace Data.Entities;

public partial class Category
{
    public int Id { get; set; }

    public string Description { get; set; } = null!;
}
