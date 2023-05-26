namespace Agri_Cultured.Models;

public partial class Expence
{
    public int ExpencesId { get; set; }

    public string ExpenceType { get; set; } = null!;

    public int ExpenceAmmount { get; set; }

    public DateOnly Date { get; set; }

    public virtual ICollection<PlantsHasUser> PlantsUsers { get; set; } = new List<PlantsHasUser>();
}
