namespace TezHealth.Application.DTOs;

public class DrugDto
{
    public int Id { get; set; }
    public Guid DrugId { get; set; }
    public Guid CategoryId { get; set; }
    public Guid? ManufacturerId { get; set; }
    public string DrugName { get; set; } = string.Empty;
    public string? GenericName { get; set; }
    public string? Strength { get; set; }
    public string? Unit { get; set; }
    public int? MinimumStockThreshold { get; set; }
    public string? StorageCondition { get; set; }
    public bool? Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
