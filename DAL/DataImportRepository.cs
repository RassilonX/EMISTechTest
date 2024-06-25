using DAL.Dtos;
using Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL;

public class DataImportRepository : IDataImportRepository
{
    private readonly DatabaseDbContext _dbContext;

    public DataImportRepository(DatabaseDbContext dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException("Context cannot be null when creating the repository");
    }

    public Task<ImportResultDto> SaveJson(List<ImportJsonDto> data)
    {
        var result = new ImportResultDto() { Success = true , FailedImports = new List<ImportJsonDto>() };
        //This feels unusual, but we are going to do a foreach loop with a try catch block in it
        //The catch block will swallow the exception and add the bad data to the FailedImports model
        //This will allow us to import the good data, and let the user know of the bad data in the file

        //First extract the person data

        //Next save the person data to the database

        //If it's failed, then add it to the result object

        //If it succeeded get their ID and use it to save the address data to the database

        //If the FailedImports list has a count greater than zero, flip the success bool to false
        if (result.FailedImports.Count > 0)
        {
            result.Success = false;
        }

        //Return the successful object
        return Task.FromResult(result);
    }
}
