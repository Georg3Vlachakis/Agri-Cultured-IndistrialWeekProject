using System;
using System.Collections.Generic;

namespace Agri_Cultured.Models;

public partial class PlantsHasUser
{
    public int PlantsPlantId { get; set; }

    public string AspnetusersId { get; set; } = null!;

    public DateOnly DatePlanted { get; set; }

    public string Location { get; set; } = null!;

    public virtual Aspnetuser Aspnetusers { get; set; } = null!;

    public virtual Plant PlantsPlant { get; set; } = null!;

    public virtual ICollection<Event> EventsEvents { get; set; } = new List<Event>();

    public virtual ICollection<Expence> ExpencesExpences { get; set; } = new List<Expence>();

    public virtual ICollection<FertPest> FertPestFertPests { get; set; } = new List<FertPest>();

    public virtual ICollection<Income> IncomeIncomes { get; set; } = new List<Income>();

    public virtual ICollection<Irrigation> IrrigationIrrigations { get; set; } = new List<Irrigation>();

    public virtual ICollection<Task> TasksTasks { get; set; } = new List<Task>();
}
