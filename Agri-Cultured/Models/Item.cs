namespace Agri_Cultured.Models;

public partial class Item
{
    public int ItemId { get; set; }

    public string ItemName { get; set; } = null!;

    public string? ItemDescription { get; set; }

    public float Cost { get; set; }

    public int FertPestFertPestId { get; set; }

    public virtual FertPest FertPestFertPest { get; set; } = null!;
}
