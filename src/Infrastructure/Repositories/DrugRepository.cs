namespace TezHealth.Infrastructure.Repositories;

using TezHealth.Application.Interfaces;
using TezHealth.Domain.Entities;
using TezHealth.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

public class DrugRepository : IDrugRepository
{
    private readonly ApplicationDbContext _context;

    public DrugRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Drug?> GetByIdAsync(int id)
    {
        return await _context.Drugs.FirstOrDefaultAsync(d => d.Id == id);
    }

    public async Task<Drug?> GetByDrugIdAsync(Guid drugId)
    {
        return await _context.Drugs.FirstOrDefaultAsync(d => d.DrugId == drugId);
    }

    public async Task<IEnumerable<Drug>> GetAllAsync()
    {
        return await _context.Drugs.ToListAsync();
    }

    public async Task<IEnumerable<Drug>> GetByCategoryIdAsync(Guid categoryId)
    {
        return await _context.Drugs
            .Where(d => d.CategoryId == categoryId)
            .ToListAsync();
    }

    public async Task<Drug> CreateAsync(Drug drug)
    {
        _context.Drugs.Add(drug);
        await _context.SaveChangesAsync();
        return drug;
    }

    public async Task<Drug> UpdateAsync(Drug drug)
    {
        _context.Drugs.Update(drug);
        await _context.SaveChangesAsync();
        return drug;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var drug = await GetByIdAsync(id);
        if (drug == null) return false;

        _context.Drugs.Remove(drug);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteByDrugIdAsync(Guid drugId)
    {
        var drug = await GetByDrugIdAsync(drugId);
        if (drug == null) return false;

        _context.Drugs.Remove(drug);
        await _context.SaveChangesAsync();
        return true;
    }
}
