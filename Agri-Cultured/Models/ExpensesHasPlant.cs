using System;
using System.Collections.Generic;

namespace Agri_Cultured.Models;

public partial class ExpensesHasPlant
{
    public int ExpensesId { get; set; }

    public int PlantsUserId { get; set; }

    public virtual PlantsHasUser PlantsUser { get; set; } = null!;
}
