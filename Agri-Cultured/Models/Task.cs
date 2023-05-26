using System;
using System.Collections.Generic;

namespace Agri_Cultured.Models;

public partial class Task
{
    public int TaskId { get; set; }

    public string TaskName { get; set; } = null!;

    public DateOnly DateStarted { get; set; }

    public int WorkerNumber { get; set; }

    public float Cost { get; set; }

    public virtual ICollection<PlantsHasUser> PlantsUsers { get; set; } = new List<PlantsHasUser>();
}
