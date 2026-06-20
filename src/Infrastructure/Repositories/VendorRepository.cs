namespace TezHealth.Infrastructure.Repositories;

using TezHealth.Application.Interfaces;
using TezHealth.Domain.Entities;
using TezHealth.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

public class VendorRepository : IVendorRepository
{
    private readonly ApplicationDbContext _context;

    public VendorRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Vendor?> GetByIdAsync(int id)
    {
        return await _context.Vendors.FirstOrDefaultAsync(v => v.Id == id);
    }

    public async Task<Vendor?> GetByVendorIdAsync(Guid vendorId)
    {
        return await _context.Vendors.FirstOrDefaultAsync(v => v.VendorId == vendorId);
    }

    public async Task<IEnumerable<Vendor>> GetAllAsync()
    {
        return await _context.Vendors.ToListAsync();
    }

    public async Task<IEnumerable<Vendor>> GetByCityAsync(string city)
    {
        return await _context.Vendors
            .Where(v => v.City == city)
            .ToListAsync();
    }

    public async Task<IEnumerable<Vendor>> GetByStateAsync(string state)
    {
        return await _context.Vendors
            .Where(v => v.State == state)
            .ToListAsync();
    }

    public async Task<IEnumerable<Vendor>> GetActiveVendorsAsync()
    {
        return await _context.Vendors
            .Where(v => v.Status == true)
            .ToListAsync();
    }

    public async Task<Vendor?> GetByEmailAsync(string email)
    {
        return await _context.Vendors
            .FirstOrDefaultAsync(v => v.Email == email);
    }

    public async Task<Vendor?> GetByPhoneNumberAsync(string phoneNumber)
    {
        return await _context.Vendors
            .FirstOrDefaultAsync(v => v.PhoneNumber == phoneNumber);
    }

    public async Task<Vendor> CreateAsync(Vendor vendor)
    {
        _context.Vendors.Add(vendor);
        await _context.SaveChangesAsync();
        return vendor;
    }

    public async Task<Vendor> UpdateAsync(Vendor vendor)
    {
        _context.Vendors.Update(vendor);
        await _context.SaveChangesAsync();
        return vendor;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var vendor = await GetByIdAsync(id);
        if (vendor == null) return false;

        _context.Vendors.Remove(vendor);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteByVendorIdAsync(Guid vendorId)
    {
        var vendor = await GetByVendorIdAsync(vendorId);
        if (vendor == null) return false;

        _context.Vendors.Remove(vendor);
        await _context.SaveChangesAsync();
        return true;
    }
}
