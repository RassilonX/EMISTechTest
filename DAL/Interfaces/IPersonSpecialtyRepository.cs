using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Database.Models;

namespace DAL.Interfaces;

public interface IPersonSpecialtyRepository
{
    public Task<List<Specialty>> ListAllSpecialtiesAsync();

    public Task<Dictionary<string, bool>> ListDoctorSpecialtyAsync(Person person);

    public Task SaveAsync(Dictionary<string, bool> specialties, int personId);

    public Task<List<Specialty>> UpdateSpecialtyTableAsync(List<Specialty> specialties);

    public Task DeleteSpecialtyAsync(int specialtyId);
}
