namespace TezHealth.Infrastructure.Persistence;

using TezHealth.Domain.Entities;
using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Drug> Drugs { get; set; } = null!;
    public DbSet<Vendor> Vendors { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Drug>(entity =>
        {
            entity.HasKey(e => e.DrugId);
            entity.Property(e => e.DrugId).IsRequired().HasDefaultValueSql("gen_random_uuid()");
            entity.Property(e => e.CategoryId).IsRequired();
            entity.Property(e => e.DrugName).IsRequired().HasMaxLength(255);
            entity.Property(e => e.GenericName).HasMaxLength(255);
            entity.Property(e => e.Strength).HasMaxLength(100);
            entity.Property(e => e.Unit).HasMaxLength(50);
            entity.Property(e => e.StorageCondition).HasColumnType("text");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.HasIndex(e => e.DrugId).IsUnique();
            entity.HasIndex(e => e.CategoryId);
        });

        modelBuilder.Entity<Vendor>(entity =>
        {
            entity.HasKey(e => e.VendorId);
            entity.Property(e => e.VendorId).IsRequired().HasDefaultValueSql("gen_random_uuid()");
            entity.Property(e => e.VendorName).IsRequired().HasMaxLength(255);
            entity.Property(e => e.ContactPerson).HasMaxLength(255);
            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.PhoneNumber).HasMaxLength(20);
            entity.Property(e => e.Address).HasColumnType("text");
            entity.Property(e => e.City).HasMaxLength(100);
            entity.Property(e => e.State).HasMaxLength(100);
            entity.Property(e => e.Pincode).HasMaxLength(10);
            entity.Property(e => e.GstNumber).HasMaxLength(15);
            entity.Property(e => e.DrugLicenseNumber).HasMaxLength(50);
            entity.Property(e => e.PaymentTerms).HasMaxLength(255);
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.HasIndex(e => e.VendorId).IsUnique();
            entity.HasIndex(e => e.Email).IsUnique();
            entity.HasIndex(e => e.PhoneNumber);
            entity.HasIndex(e => e.City);
            entity.HasIndex(e => e.State);
        });
    }
}
