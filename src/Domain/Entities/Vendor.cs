using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TezHealth.Domain.Entities;

[Table("vendors")]
public class Vendor : Common.BaseEntity
{
    [Key]
    public Guid VendorId { get; set; } = Guid.NewGuid();
    public string VendorName { get; set; } = string.Empty;
    public string? ContactPerson { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Address { get; set; }
    public string? City { get; set; }
    public string? State { get; set; }
    public string? Pincode { get; set; }
    public string? GstNumber { get; set; }
    public string? DrugLicenseNumber { get; set; }
    public string? PaymentTerms { get; set; }
    public bool? Status { get; set; }
}
