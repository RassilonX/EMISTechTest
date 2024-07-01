using DAL.Interfaces;
using Database;
using Database.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL;

public class SpecialtyRepository : ISpecialtyRepository
{
    private readonly DatabaseDbContext _dbContext;

    public SpecialtyRepository(DatabaseDbContext dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException("Context cannot be null when creating the repository");
    }

    public async Task<List<Specialty>> ListAllSpecialtiesAsync()
    {
        return await _dbContext.Specialties.ToListAsync();
    }

    public async Task DeleteSpecialtyAsync(int specialtyId)
    {
        using (var transaction = _dbContext.Database.BeginTransaction())
        {
            try
            {
                var specialty = await _dbContext.Specialties.FindAsync(specialtyId);
                if (specialty != null)
                {
                    _dbContext.Specialties.Remove(specialty);
                    await _dbContext.SaveChangesAsync();

                    transaction.Commit();
                }
            }
            catch (Exception ex)
            {
                transaction.Rollback();
            }
        }
    }

    public async Task SaveNewSpecialtyAsync(string specialtyName)
    {
        using (var transaction = _dbContext.Database.BeginTransaction())
        {
            try
            {
                var specialty = new Specialty() { SpecialtyName = specialtyName };
                _dbContext.Specialties.Add(specialty);

                await _dbContext.SaveChangesAsync();
                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
            }
        }
    }
}
