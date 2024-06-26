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

    public async Task<List<DoctorSpecialty>> ListDoctorSpecialtyAsync(Person person)
    {
        return new List<DoctorSpecialty>();
    }

    public Task SaveAsync()
    {
        throw new NotImplementedException();
    }
}
