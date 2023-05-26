using System;
using System.Collections.Generic;

namespace Agri_Cultured.Models;

public partial class Event
{
    public int EventId { get; set; }

    public string Name { get; set; } = null!;

    public DateOnly Date { get; set; }

    public string Damage { get; set; } = null!;

    public int PercDamage { get; set; }

    public string Comments { get; set; } = null!;

    public virtual ICollection<PlantsHasUser> PlantsUsers { get; set; } = new List<PlantsHasUser>();
}
