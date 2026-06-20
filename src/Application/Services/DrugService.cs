namespace TezHealth.Application.Services;

using TezHealth.Application.DTOs;
using TezHealth.Application.Interfaces;
using TezHealth.Application.Exceptions;
using TezHealth.Domain.Entities;

public class DrugService : IDrugService
{
    private readonly IDrugRepository _repository;

    public DrugService(IDrugRepository repository)
    {
        _repository = repository;
    }

    public async Task<DrugDto?> GetDrugByIdAsync(int id)
    {
        var drug = await _repository.GetByIdAsync(id);
        if (drug == null)
            throw new NotFoundException("Drug", id);

        return MapToDto(drug);
    }

    public async Task<DrugDto?> GetDrugByDrugIdAsync(Guid drugId)
    {
        var drug = await _repository.GetByDrugIdAsync(drugId);
        if (drug == null)
            throw new NotFoundException("Drug", drugId);

        return MapToDto(drug);
    }

    public async Task<IEnumerable<DrugDto>> GetAllDrugsAsync()
    {
        var drugs = await _repository.GetAllAsync();
        return drugs.Select(MapToDto);
    }

    public async Task<IEnumerable<DrugDto>> GetDrugsByCategoryAsync(Guid categoryId)
    {
        if (categoryId == Guid.Empty)
            throw new BadRequestException("Category ID cannot be empty.");

        var drugs = await _repository.GetByCategoryIdAsync(categoryId);
        return drugs.Select(MapToDto);
    }

    public async Task<DrugDto> CreateDrugAsync(DrugDto drugDto)
    {
        ValidateCreateDrugInput(drugDto);

        var drug = new Drug
        {
            DrugId = drugDto.DrugId != Guid.Empty ? drugDto.DrugId : Guid.NewGuid(),
            CategoryId = drugDto.CategoryId,
            ManufacturerId = drugDto.ManufacturerId,
            DrugName = drugDto.DrugName,
            GenericName = drugDto.GenericName,
            Strength = drugDto.Strength,
            Unit = drugDto.Unit,
            MinimumStockThreshold = drugDto.MinimumStockThreshold,
            StorageCondition = drugDto.StorageCondition,
            Status = drugDto.Status
        };

        var createdDrug = await _repository.CreateAsync(drug);
        return MapToDto(createdDrug);
    }

    public async Task<DrugDto> UpdateDrugAsync(int id, DrugDto drugDto)
    {
        ValidateUpdateDrugInput(drugDto);

        var drug = await _repository.GetByIdAsync(id);
        if (drug == null)
            throw new NotFoundException("Drug", id);

        drug.CategoryId = drugDto.CategoryId;
        drug.ManufacturerId = drugDto.ManufacturerId;
        drug.DrugName = drugDto.DrugName;
        drug.GenericName = drugDto.GenericName;
        drug.Strength = drugDto.Strength;
        drug.Unit = drugDto.Unit;
        drug.MinimumStockThreshold = drugDto.MinimumStockThreshold;
        drug.StorageCondition = drugDto.StorageCondition;
        drug.Status = drugDto.Status;
        drug.UpdatedAt = DateTime.UtcNow;

        var updatedDrug = await _repository.UpdateAsync(drug);
        return MapToDto(updatedDrug);
    }

    public async Task<bool> DeleteDrugAsync(int id)
    {
        var result = await _repository.DeleteAsync(id);
        if (!result)
            throw new NotFoundException("Drug", id);

        return result;
    }

    public async Task<bool> DeleteDrugByDrugIdAsync(Guid drugId)
    {
        if (drugId == Guid.Empty)
            throw new BadRequestException("Drug ID cannot be empty.");

        var result = await _repository.DeleteByDrugIdAsync(drugId);
        if (!result)
            throw new NotFoundException("Drug", drugId);

        return result;
    }

    private static void ValidateCreateDrugInput(DrugDto drugDto)
    {
        var errors = new Dictionary<string, string[]>();

        if (string.IsNullOrWhiteSpace(drugDto.DrugName))
            errors.Add(nameof(drugDto.DrugName), new[] { "Drug name is required." });

        if (drugDto.CategoryId == Guid.Empty)
            errors.Add(nameof(drugDto.CategoryId), new[] { "Category ID is required and cannot be empty." });

        if (errors.Count > 0)
            throw new ValidationException(errors);
    }

    private static void ValidateUpdateDrugInput(DrugDto drugDto)
    {
        var errors = new Dictionary<string, string[]>();

        if (string.IsNullOrWhiteSpace(drugDto.DrugName))
            errors.Add(nameof(drugDto.DrugName), new[] { "Drug name is required." });

        if (drugDto.CategoryId == Guid.Empty)
            errors.Add(nameof(drugDto.CategoryId), new[] { "Category ID is required and cannot be empty." });

        if (errors.Count > 0)
            throw new ValidationException(errors);
    }

    private static DrugDto MapToDto(Drug drug)
    {
        return new DrugDto
        {
            Id = drug.Id,
            DrugId = drug.DrugId,
            CategoryId = drug.CategoryId,
            ManufacturerId = drug.ManufacturerId,
            DrugName = drug.DrugName,
            GenericName = drug.GenericName,
            Strength = drug.Strength,
            Unit = drug.Unit,
            MinimumStockThreshold = drug.MinimumStockThreshold,
            StorageCondition = drug.StorageCondition,
            Status = drug.Status,
            CreatedAt = drug.CreatedAt,
            UpdatedAt = drug.UpdatedAt
        };
    }
}
