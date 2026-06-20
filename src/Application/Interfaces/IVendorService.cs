namespace TezHealth.Application.Interfaces;

using TezHealth.Application.DTOs;

public interface IVendorService
{
    Task<VendorDto?> GetVendorByIdAsync(int id);
    Task<VendorDto?> GetVendorByVendorIdAsync(Guid vendorId);
    Task<IEnumerable<VendorDto>> GetAllVendorsAsync();
    Task<IEnumerable<VendorDto>> GetVendorsByCityAsync(string city);
    Task<IEnumerable<VendorDto>> GetVendorsByStateAsync(string state);
    Task<IEnumerable<VendorDto>> GetActiveVendorsAsync();
    Task<VendorDto?> GetVendorByEmailAsync(string email);
    Task<VendorDto?> GetVendorByPhoneNumberAsync(string phoneNumber);
    Task<VendorDto> CreateVendorAsync(VendorDto vendorDto);
    Task<VendorDto> UpdateVendorAsync(int id, VendorDto vendorDto);
    Task<bool> DeleteVendorAsync(int id);
    Task<bool> DeleteVendorByVendorIdAsync(Guid vendorId);
}
