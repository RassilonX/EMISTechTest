using DAL.Interfaces;
using Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL;

public class PersonSpecialtyRepository : IPersonSpecialtyRepository
{
    public Task<List<DoctorSpecialty>> ListAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task SaveAsync()
    {
        throw new NotImplementedException();
    }
}
