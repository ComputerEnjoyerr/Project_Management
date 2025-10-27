using System;
using System.Collections.Generic;

namespace Project_Management.Models;

public partial class Stage
{
    public int StageId { get; set; }

    public int ProjectId { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public string? Status { get; set; }

    public DateOnly? StartDate { get; set; }

    public DateOnly? EndDate { get; set; }

    public virtual Project Project { get; set; } = null!;
}
