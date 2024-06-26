using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Database.Models;

namespace DAL.Interfaces;

public interface IPersonSpecialtyRepository
{
    public Task<List<DoctorSpecialty>> ListAllAsync();

    public Task SaveAsync();
}
