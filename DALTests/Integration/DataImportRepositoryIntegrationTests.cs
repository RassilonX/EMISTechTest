﻿using DAL;
using DAL.Dtos;
using Database;
using Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DALTests.Integration;

public class DataImportRepositoryIntegrationTests
{
    private readonly DatabaseDbContext _dbContext;
    private readonly DataImportRepository _repository;

    public DataImportRepositoryIntegrationTests()
    {
        var options = CreateOptions();

        _dbContext = new DatabaseDbContext(options);
        _repository = new DataImportRepository(_dbContext);
    }

    [Fact]
    public async Task SaveJson_Succeeds_WhenDataIsValid()
    {
        // Arrange
        var data = new List<ImportJsonDto>
        {
            new ImportJsonDto
            {
                FirstName = "John",
                LastName = "Doe",
                GMC = 123456,
                Address = new List<Address>
                {
                    new Address
                    {
                        City = "London",
                        Line1 = "123 Main St",
                        Postcode = "SW1 1AA"
                    }
                }
            }
        };

        // Act
        var result = await _repository.SaveJson(data);
        var person = _dbContext.People.FirstOrDefault(p => p.GMC == 123456);
        var address = _dbContext.Addresses.FirstOrDefault(a => a.PersonId == person.Id);

        //We remove the data we inserted to make the test repeatable
        _dbContext.People.Remove(person);
        _dbContext.Addresses.Remove(address);
        _dbContext.SaveChanges();

        // Assert
        Assert.True(result.Success);

        // Verify data was saved to database
        Assert.NotNull(person);
        Assert.Equal("John", person.FirstName);
        Assert.Equal("Doe", person.LastName);

        Assert.NotNull(address);
        Assert.Equal("London", address.City);
        Assert.Equal("123 Main St", address.Line1);
        Assert.Equal("SW1 1AA", address.Postcode);
    }

    [Fact]
    public async Task SaveJson_Fails_WhenDataAlreadyExists()
    {
        // Arrange
        var data = new List<ImportJsonDto>
        {
            new ImportJsonDto
            {
                FirstName = "John",
                LastName = "Doe",
                GMC = 123456,
                Address = new List<Address>
                {
                    new Address
                    {
                        City = "London",
                        Line1 = "123 Main St",
                        Postcode = "SW1 1AA"
                    }
                }
            }
        };

        // Create a person with the same GMC in the database
        var existingPerson = new Person
        {
            FirstName = "John",
            LastName = "Doe",
            GMC = 123456
        };

        _dbContext.People.Add(existingPerson);
        _dbContext.SaveChanges();

        // Act
        var result = await _repository.SaveJson(data);

        //We remove the data we inserted to make the test repeatable
        _dbContext.People.Remove(existingPerson);
        _dbContext.SaveChanges();

        // Assert
        Assert.False(result.Success);

        var address = _dbContext.Addresses.FirstOrDefault(a => a.PersonId == existingPerson.Id);
        Assert.Null(address);
    }

    private DbContextOptions<DatabaseDbContext> CreateOptions()
    {
        var builder = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
        var configurationRoot = builder.Build();
        var connectionString = configurationRoot.GetConnectionString("DB_CONNECTION_STRING");

        var options = new DbContextOptionsBuilder<DatabaseDbContext>()
                 .UseSqlServer(connectionString).Options;
        return options;
    }
}
