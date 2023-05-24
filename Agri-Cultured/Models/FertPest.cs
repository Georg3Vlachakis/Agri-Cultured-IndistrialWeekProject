using System;
using System.Collections.Generic;

namespace Agri_Cultured.Models;

public partial class FertPest
{
    public int FertPestId { get; set; }

    public bool Type { get; set; }

    public string ProductName { get; set; } = null!;

    public DateOnly Date { get; set; }

    public virtual ICollection<Item> Items { get; set; } = new List<Item>();

    public virtual ICollection<PlantsHasUser> PlantsUsers { get; set; } = new List<PlantsHasUser>();
}
