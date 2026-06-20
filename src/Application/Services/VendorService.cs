namespace TezHealth.Application.Services;

using TezHealth.Application.DTOs;
using TezHealth.Application.Interfaces;
using TezHealth.Application.Exceptions;
using TezHealth.Domain.Entities;

public class VendorService : IVendorService
{
    private readonly IVendorRepository _repository;

    public VendorService(IVendorRepository repository)
    {
        _repository = repository;
    }

    public async Task<VendorDto?> GetVendorByIdAsync(int id)
    {
        var vendor = await _repository.GetByIdAsync(id);
        if (vendor == null)
            throw new NotFoundException("Vendor", id);

        return MapToDto(vendor);
    }

    public async Task<VendorDto?> GetVendorByVendorIdAsync(Guid vendorId)
    {
        var vendor = await _repository.GetByVendorIdAsync(vendorId);
        if (vendor == null)
            throw new NotFoundException("Vendor", vendorId);

        return MapToDto(vendor);
    }

    public async Task<IEnumerable<VendorDto>> GetAllVendorsAsync()
    {
        var vendors = await _repository.GetAllAsync();
        return vendors.Select(MapToDto);
    }

    public async Task<IEnumerable<VendorDto>> GetVendorsByCityAsync(string city)
    {
        if (string.IsNullOrWhiteSpace(city))
            throw new BadRequestException("City cannot be empty.");

        var vendors = await _repository.GetByCityAsync(city);
        return vendors.Select(MapToDto);
    }

    public async Task<IEnumerable<VendorDto>> GetVendorsByStateAsync(string state)
    {
        if (string.IsNullOrWhiteSpace(state))
            throw new BadRequestException("State cannot be empty.");

        var vendors = await _repository.GetByStateAsync(state);
        return vendors.Select(MapToDto);
    }

    public async Task<IEnumerable<VendorDto>> GetActiveVendorsAsync()
    {
        var vendors = await _repository.GetActiveVendorsAsync();
        return vendors.Select(MapToDto);
    }

    public async Task<VendorDto?> GetVendorByEmailAsync(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            throw new BadRequestException("Email cannot be empty.");

        var vendor = await _repository.GetByEmailAsync(email);
        if (vendor == null)
            throw new NotFoundException("Vendor", $"email: {email}");

        return MapToDto(vendor);
    }

    public async Task<VendorDto?> GetVendorByPhoneNumberAsync(string phoneNumber)
    {
        if (string.IsNullOrWhiteSpace(phoneNumber))
            throw new BadRequestException("Phone number cannot be empty.");

        var vendor = await _repository.GetByPhoneNumberAsync(phoneNumber);
        if (vendor == null)
            throw new NotFoundException("Vendor", $"phone: {phoneNumber}");

        return MapToDto(vendor);
    }

    public async Task<VendorDto> CreateVendorAsync(VendorDto vendorDto)
    {
        ValidateCreateVendorInput(vendorDto);

        // Check for duplicate email if provided
        if (!string.IsNullOrWhiteSpace(vendorDto.Email))
        {
            var existingByEmail = await _repository.GetByEmailAsync(vendorDto.Email);
            if (existingByEmail != null)
                throw new ConflictException($"A vendor with email '{vendorDto.Email}' already exists.");
        }

        // Check for duplicate phone if provided
        if (!string.IsNullOrWhiteSpace(vendorDto.PhoneNumber))
        {
            var existingByPhone = await _repository.GetByPhoneNumberAsync(vendorDto.PhoneNumber);
            if (existingByPhone != null)
                throw new ConflictException($"A vendor with phone number '{vendorDto.PhoneNumber}' already exists.");
        }

        var vendor = new Vendor
        {
            VendorId = vendorDto.VendorId != Guid.Empty ? vendorDto.VendorId : Guid.NewGuid(),
            VendorName = vendorDto.VendorName,
            ContactPerson = vendorDto.ContactPerson,
            Email = vendorDto.Email,
            PhoneNumber = vendorDto.PhoneNumber,
            Address = vendorDto.Address,
            City = vendorDto.City,
            State = vendorDto.State,
            Pincode = vendorDto.Pincode,
            GstNumber = vendorDto.GstNumber,
            DrugLicenseNumber = vendorDto.DrugLicenseNumber,
            PaymentTerms = vendorDto.PaymentTerms,
            Status = vendorDto.Status
        };

        var createdVendor = await _repository.CreateAsync(vendor);
        return MapToDto(createdVendor);
    }

    public async Task<VendorDto> UpdateVendorAsync(int id, VendorDto vendorDto)
    {
        ValidateUpdateVendorInput(vendorDto);

        var vendor = await _repository.GetByIdAsync(id);
        if (vendor == null)
            throw new NotFoundException("Vendor", id);

        // Check for duplicate email if email is being changed
        if (!string.IsNullOrWhiteSpace(vendorDto.Email) && vendor.Email != vendorDto.Email)
        {
            var existingByEmail = await _repository.GetByEmailAsync(vendorDto.Email);
            if (existingByEmail != null)
                throw new ConflictException($"A vendor with email '{vendorDto.Email}' already exists.");
        }

        // Check for duplicate phone if phone is being changed
        if (!string.IsNullOrWhiteSpace(vendorDto.PhoneNumber) && vendor.PhoneNumber != vendorDto.PhoneNumber)
        {
            var existingByPhone = await _repository.GetByPhoneNumberAsync(vendorDto.PhoneNumber);
            if (existingByPhone != null)
                throw new ConflictException($"A vendor with phone number '{vendorDto.PhoneNumber}' already exists.");
        }

        vendor.VendorName = vendorDto.VendorName;
        vendor.ContactPerson = vendorDto.ContactPerson;
        vendor.Email = vendorDto.Email;
        vendor.PhoneNumber = vendorDto.PhoneNumber;
        vendor.Address = vendorDto.Address;
        vendor.City = vendorDto.City;
        vendor.State = vendorDto.State;
        vendor.Pincode = vendorDto.Pincode;
        vendor.GstNumber = vendorDto.GstNumber;
        vendor.DrugLicenseNumber = vendorDto.DrugLicenseNumber;
        vendor.PaymentTerms = vendorDto.PaymentTerms;
        vendor.Status = vendorDto.Status;
        vendor.UpdatedAt = DateTime.UtcNow;

        var updatedVendor = await _repository.UpdateAsync(vendor);
        return MapToDto(updatedVendor);
    }

    public async Task<bool> DeleteVendorAsync(int id)
    {
        var result = await _repository.DeleteAsync(id);
        if (!result)
            throw new NotFoundException("Vendor", id);

        return result;
    }

    public async Task<bool> DeleteVendorByVendorIdAsync(Guid vendorId)
    {
        if (vendorId == Guid.Empty)
            throw new BadRequestException("Vendor ID cannot be empty.");

        var result = await _repository.DeleteByVendorIdAsync(vendorId);
        if (!result)
            throw new NotFoundException("Vendor", vendorId);

        return result;
    }

    private static void ValidateCreateVendorInput(VendorDto vendorDto)
    {
        var errors = new Dictionary<string, string[]>();

        if (string.IsNullOrWhiteSpace(vendorDto.VendorName))
            errors.Add(nameof(vendorDto.VendorName), new[] { "Vendor name is required." });

        if (errors.Count > 0)
            throw new ValidationException(errors);
    }

    private static void ValidateUpdateVendorInput(VendorDto vendorDto)
    {
        var errors = new Dictionary<string, string[]>();

        if (string.IsNullOrWhiteSpace(vendorDto.VendorName))
            errors.Add(nameof(vendorDto.VendorName), new[] { "Vendor name is required." });

        if (errors.Count > 0)
            throw new ValidationException(errors);
    }

    private static VendorDto MapToDto(Vendor vendor)
    {
        return new VendorDto
        {
            VendorId = vendor.VendorId,
            VendorName = vendor.VendorName,
            ContactPerson = vendor.ContactPerson,
            Email = vendor.Email,
            PhoneNumber = vendor.PhoneNumber,
            Address = vendor.Address,
            City = vendor.City,
            State = vendor.State,
            Pincode = vendor.Pincode,
            GstNumber = vendor.GstNumber,
            DrugLicenseNumber = vendor.DrugLicenseNumber,
            PaymentTerms = vendor.PaymentTerms,
            Status = vendor.Status,
            CreatedAt = vendor.CreatedAt
        };
    }
}
