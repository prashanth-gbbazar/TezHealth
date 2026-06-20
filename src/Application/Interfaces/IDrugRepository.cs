namespace TezHealth.Application.Interfaces;

using TezHealth.Domain.Entities;

public interface IDrugRepository
{
    Task<Drug?> GetByIdAsync(int id);
    Task<Drug?> GetByDrugIdAsync(Guid drugId);
    Task<IEnumerable<Drug>> GetAllAsync();
    Task<IEnumerable<Drug>> GetByCategoryIdAsync(Guid categoryId);
    Task<Drug> CreateAsync(Drug drug);
    Task<Drug> UpdateAsync(Drug drug);
    Task<bool> DeleteAsync(int id);
    Task<bool> DeleteByDrugIdAsync(Guid drugId);
}
