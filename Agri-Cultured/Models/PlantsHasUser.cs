using System;
using System.Collections.Generic;

namespace Agri_Cultured.Models;

public partial class PlantsHasUser
{
    public int PlantsUserId { get; set; }

    public int PlantsPlantId { get; set; }

    public string AspnetusersId { get; set; } = null!;

    public DateOnly DatePlanted { get; set; }

    public string Location { get; set; } = null!;

    public string Description { get; set; } = null!;

    public virtual Aspnetuser Aspnetusers { get; set; } = null!;

    public virtual Plant PlantsPlant { get; set; } = null!;

    public virtual ICollection<Event> Events { get; set; } = new List<Event>();

    public virtual ICollection<Expense> Expenses { get; set; } = new List<Expense>();

    public virtual ICollection<FertPest> FertPests { get; set; } = new List<FertPest>();

    public virtual ICollection<Irrigation> Iirrigations { get; set; } = new List<Irrigation>();

    public virtual ICollection<Income> Incomes { get; set; } = new List<Income>();

    public virtual ICollection<Task> Tasks { get; set; } = new List<Task>();
}
