namespace Agri_Cultured.Models;

public partial class Plant
{
    public int PlantId { get; set; }

    public string PlantName { get; set; } = null!;

    public string PlantType { get; set; } = null!;

    public virtual ICollection<PlantsHasUser> PlantsHasUsers { get; set; } = new List<PlantsHasUser>();
}
