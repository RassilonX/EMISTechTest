using DAL.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL;

public class DataImportRepository : IDataImportRepository
{
    public Task<bool> SaveJson(List<ImportJsonDto> data)
    {
        return Task.FromResult(true);
    }
}
