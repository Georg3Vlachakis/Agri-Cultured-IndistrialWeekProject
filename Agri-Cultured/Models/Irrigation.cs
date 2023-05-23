using System;
using System.Collections.Generic;

namespace Agri_Cultured.Models;

public partial class Irrigation
{
    public int IrrigationsId { get; set; }

    public DateOnly Date { get; set; }

    public int Hours { get; set; }

    public int? Cost { get; set; }

    public virtual ICollection<PlantsHasUser> PlantsHasUsers { get; set; } = new List<PlantsHasUser>();
}
