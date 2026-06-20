using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TezHealth.Domain.Entities;

[Table("drugs")]
public class Drug : Common.BaseEntity
{
    [Key]
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
