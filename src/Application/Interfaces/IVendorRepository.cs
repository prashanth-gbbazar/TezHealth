namespace TezHealth.Application.Interfaces;

using TezHealth.Domain.Entities;

public interface IVendorRepository
{
    Task<Vendor?> GetByIdAsync(int id);
    Task<Vendor?> GetByVendorIdAsync(Guid vendorId);
    Task<IEnumerable<Vendor>> GetAllAsync();
    Task<IEnumerable<Vendor>> GetByCityAsync(string city);
    Task<IEnumerable<Vendor>> GetByStateAsync(string state);
    Task<IEnumerable<Vendor>> GetActiveVendorsAsync();
    Task<Vendor?> GetByEmailAsync(string email);
    Task<Vendor?> GetByPhoneNumberAsync(string phoneNumber);
    Task<Vendor> CreateAsync(Vendor vendor);
    Task<Vendor> UpdateAsync(Vendor vendor);
    Task<bool> DeleteAsync(int id);
    Task<bool> DeleteByVendorIdAsync(Guid vendorId);
}
