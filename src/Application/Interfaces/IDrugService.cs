namespace TezHealth.Application.Interfaces;

using TezHealth.Application.DTOs;

public interface IDrugService
{
    Task<DrugDto?> GetDrugByIdAsync(int id);
    Task<DrugDto?> GetDrugByDrugIdAsync(Guid drugId);
    Task<IEnumerable<DrugDto>> GetAllDrugsAsync();
    Task<IEnumerable<DrugDto>> GetDrugsByCategoryAsync(Guid categoryId);
    Task<DrugDto> CreateDrugAsync(DrugDto drugDto);
    Task<DrugDto> UpdateDrugAsync(int id, DrugDto drugDto);
    Task<bool> DeleteDrugAsync(int id);
    Task<bool> DeleteDrugByDrugIdAsync(Guid drugId);
}
