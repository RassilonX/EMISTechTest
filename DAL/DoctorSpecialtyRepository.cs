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

public class DoctorSpecialtyRepository : IDoctorSpecialtyRepository
{
    private readonly DatabaseDbContext _dbContext;

    public DoctorSpecialtyRepository(DatabaseDbContext dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException("Context cannot be null when creating the repository");
    }

    public async Task<Dictionary<string, bool>> ListDoctorSpecialtyByPersonAsync(Person person)
    {
        var result = new Dictionary<string, bool>();
        var specialtiesList =  await _dbContext.Specialties.ToListAsync();

        var doctorSpecialties = await _dbContext.DoctorSpecialties.Where(p => p.PersonId == person.Id).ToListAsync();

        foreach (var item in specialtiesList)
        {
            result.Add(
                item.SpecialtyName,
                doctorSpecialties.Any(ds => ds.SpecialtyId == item.Id)
                );
        }

        return result;
    }

    public async Task SaveAsync(Dictionary<string, bool> specialties, int personId)
    {
        using (var transaction = _dbContext.Database.BeginTransaction())
        {
            try
            {
                var doctorSpecialties = await _dbContext.DoctorSpecialties.Where(p => p.PersonId == personId).ToListAsync();
                var specialtiesList = await _dbContext.Specialties.ToListAsync();

                foreach (var item in doctorSpecialties)
                {
                    _dbContext.Remove(item);
                }

                foreach (var item in specialties)
                {
                    if (item.Value)
                    {
                        _dbContext.DoctorSpecialties.Add(new DoctorSpecialty
                        {
                            PersonId = personId,
                            SpecialtyId = specialtiesList.Where(sl => sl.SpecialtyName == item.Key).Single().Id
                        });
                    }
                }

                _dbContext.SaveChanges();
                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
            }

        }
    }
}
