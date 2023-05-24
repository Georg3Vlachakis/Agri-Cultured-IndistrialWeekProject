using System;
using System.Collections.Generic;

namespace Agri_Cultured.Models;

public partial class Income
{
    public int IncomeId { get; set; }

    public DateOnly Date { get; set; }

    public int Income1 { get; set; }

    public virtual ICollection<PlantsHasUser> PlantsUsers { get; set; } = new List<PlantsHasUser>();
}
