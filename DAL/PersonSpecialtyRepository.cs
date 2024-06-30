﻿using DAL.Interfaces;
using Database;
using Database.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL;

public class PersonSpecialtyRepository : IPersonSpecialtyRepository
{
    private readonly DatabaseDbContext _dbContext;

    public PersonSpecialtyRepository(DatabaseDbContext dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException("Context cannot be null when creating the repository");
    }

    public async Task<List<Specialty>> ListAllSpecialtiesAsync()
    {
        return await _dbContext.Specialties.ToListAsync();
    }

    public async Task<Dictionary<string, bool>> ListDoctorSpecialtyAsync(Person person)
    {
        var result = new Dictionary<string, bool>();
        var specialtiesList =  await ListAllSpecialtiesAsync();

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
                var specialtiesList = await ListAllSpecialtiesAsync();

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

    //These are going to go in a separate repo I think
    public async Task<List<Specialty>> UpdateSpecialtyTableAsync(List<Specialty> specialties)
    {
        var result = specialties;

        using (var transaction = _dbContext.Database.BeginTransaction())
        {
            try
            {
                _dbContext.Specialties.UpdateRange(result);
                await _dbContext.SaveChangesAsync();

                transaction.Commit();
            }
            catch (Exception ex) 
            {
                transaction.Rollback();
            }
        }

        return result;
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
}
