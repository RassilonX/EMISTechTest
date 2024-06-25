﻿using DAL.Dtos;
using Database;
using Database.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
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
        var result = new ImportResultDto() { Success = true, FailedImports = new List<ImportJsonDto>() };

        //Use a transaction approach for the whole file, so that we can roll it back if needed
        using (var transaction = _dbContext.Database.BeginTransaction())
        {
            try
            {
                foreach (var item in data)
                {
                    //First extract the person data
                    var person = new Person()
                    {
                        FirstName = SanitiseStringInput(item.FirstName),
                        LastName = SanitiseStringInput(item.LastName),
                        GMC = item.GMC
                    };

                    //Next save the person data to the database
                    _dbContext.People.Add(person);
                    _dbContext.SaveChanges();

                    //If it succeeded get their ID and use it to save the address data to the database
                    int personId = person.Id;

                    foreach (var address in item.Address)
                    {
                        var addressEntity = new Address()
                        {
                            City = SanitiseStringInput(address.City),
                            Line1 = SanitiseStringInput(address.Line1),
                            Postcode = SanitiseStringInput(address.Postcode).ToUpper(),
                            PersonId = personId
                        };

                        _dbContext.Addresses.Add(addressEntity);
                    }

                    _dbContext.SaveChanges();
                }

                // Commit the transaction once all the data has been accepted
                transaction.Commit();
            }
            catch (Exception ex)
            {
                //If it's failed, then add it to the result object
                result.Success = false;

                transaction.Rollback();
            }
        }

        //Return the successful object
        return Task.FromResult(result);
    }

    private string SanitiseStringInput(string input)
    {
        // Remove special characters, allowing the - character through because of double barelled names
        var cleanString = new string(input.Where(
            c => char.IsLetterOrDigit(c) || 
                    char.IsWhiteSpace(c) ||
                    c == '-').ToArray());

        var textInfo = new CultureInfo("en-GB", false).TextInfo;

        return textInfo.ToTitleCase(cleanString);
    }
}
