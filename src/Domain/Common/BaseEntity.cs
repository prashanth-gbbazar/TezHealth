using System.ComponentModel.DataAnnotations.Schema;

namespace TezHealth.Domain.Common;

public abstract class BaseEntity
{
    [NotMapped]
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
}
