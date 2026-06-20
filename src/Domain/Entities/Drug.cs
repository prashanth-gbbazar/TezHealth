namespace TezHealth.Domain.Entities;

public class Drug : Common.BaseEntity
{
    public Guid DrugId { get; set; } = Guid.NewGuid();
    public Guid CategoryId { get; set; }
    public Guid? ManufacturerId { get; set; }
    public string DrugName { get; set; } = string.Empty;
    public string? GenericName { get; set; }
    public string? Strength { get; set; }
    public string? Unit { get; set; }
    public int? MinimumStockThreshold { get; set; }
    public string? StorageCondition { get; set; }
    public bool? Status { get; set; }
}
