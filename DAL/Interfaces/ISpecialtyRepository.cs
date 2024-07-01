using Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces;

public interface ISpecialtyRepository
{
    public Task<List<Specialty>> ListAllSpecialtiesAsync();

    public Task DeleteAsync(int specialtyId);

    public Task SaveAsync(string specialtyName);
}
