using DAL;
using DAL.Dtos;
using Database;
using Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DALTests.Integration;

public class PersonSpecialtyRepositoryIntegrationTests
{
    private readonly DatabaseDbContext _dbContext;
    private readonly PersonSpecialtyRepository _repository;
    private readonly DataImportRepository _dataImportRepository;

    public PersonSpecialtyRepositoryIntegrationTests()
    {
        var options = CreateOptions();

        _dbContext = new DatabaseDbContext(options);
        _repository = new PersonSpecialtyRepository(_dbContext);
        _dataImportRepository = new DataImportRepository(_dbContext);
    }

    [Fact]
    public async Task ListAllSpecialtiesAsync_SafelyReturnsListOfSpecialties()
    {
        // Arrange - Act
        var result = await _repository.ListAllSpecialtiesAsync();

        // Assert
        Assert.NotNull(result);
        Assert.IsType<List<Specialty>>(result);
    }

    [Fact]
    public async Task ListDoctorSpecialtyAsync_ReturnsDictionaryOfSpecialties()
    {
        // Arrange
        var data = new List<ImportJsonDto>
        {
            new ImportJsonDto
            {
                FirstName = "John",
                LastName = "Doe",
                GMC = 2345678
            }
        };

        var importedData = await _dataImportRepository.SaveJson(data);
        var person = _dbContext.People.FirstOrDefault(p => p.GMC == 2345678);

        var specialtyCount = _dbContext.Specialties.Count();

        // Act
        var result = await _repository.ListDoctorSpecialtyAsync(person);

        //We remove the data we inserted to make the test repeatable
        _dbContext.People.Remove(person);
        _dbContext.SaveChanges();

        //Assert
        Assert.NotNull(result);
        Assert.True(result.Any());
        Assert.Equal(specialtyCount, result.Count());
        Assert.IsType<Dictionary<string, bool>>(result);
    }

    [Fact]
    public async Task ListDoctorSpecialtyAsync_ReturnsDictionaryWithExpectedTrueValues()
    {
        // Arrange
        var data = new List<ImportJsonDto>
        {
            new ImportJsonDto
            {
                FirstName = "John",
                LastName = "Doe",
                GMC = 3345678
            }
        };

        var importedData = await _dataImportRepository.SaveJson(data);
        var person = _dbContext.People.FirstOrDefault(p => p.GMC == 3345678);
        var specialtyCount = _dbContext.Specialties.Count();

        var specialty1 = new DoctorSpecialty() { PersonId = person.Id, SpecialtyId = 3 };
        var specialty2 = new DoctorSpecialty() { PersonId = person.Id, SpecialtyId = 4 };

        _dbContext.DoctorSpecialties.Add(specialty1);
        _dbContext.DoctorSpecialties.Add(specialty2);
        _dbContext.SaveChanges();

        //Act
        var result = await _repository.ListDoctorSpecialtyAsync(person);
        int trueCount = result.Count(r => r.Value);

        //We remove the data we inserted to make the test repeatable
        _dbContext.People.Remove(person);
        _dbContext.DoctorSpecialties.Remove(specialty1);
        _dbContext.DoctorSpecialties.Remove(specialty2);
        _dbContext.SaveChanges();

        //Assert
        Assert.NotNull(result);
        Assert.True(result.Any());
        Assert.Equal(specialtyCount, result.Count());
        Assert.IsType<Dictionary<string, bool>>(result);
        Assert.Equal(2, trueCount);
    }

    [Fact]
    public async Task SaveAsync_NullSpecialtyThrowsException() 
        => Assert.ThrowsAsync<ArgumentNullException>(() => _repository.SaveAsync(null, 0));

    [Fact]
    public async Task SaveAsync_DataIsSavedCorrectly()
    {
        // Arrange
        var data = new List<ImportJsonDto>
        {
            new ImportJsonDto
            {
                FirstName = "John",
                LastName = "Doe",
                GMC = 4345678
            }
        };

        var importedData = await _dataImportRepository.SaveJson(data);
        var person = _dbContext.People.FirstOrDefault(p => p.GMC == 4345678);

        var specialties = await _repository.ListDoctorSpecialtyAsync(person);
        specialties["Anaesthetics"] = true;
        specialties["Cardiology"] = true;
        specialties["Dermatology"] = true;

        // Act
        await _repository.SaveAsync(specialties, person.Id);

        var result = _dbContext.DoctorSpecialties.Where(p => p.PersonId == person.Id).ToList();

        _dbContext.People.Remove(person);
        var removedEntities = _dbContext.DoctorSpecialties.Where(p => p.PersonId == person.Id).ToList();
        _dbContext.DoctorSpecialties.RemoveRange(removedEntities);
        _dbContext.SaveChanges();

        // Assert
        Assert.NotNull(result);
        Assert.True(result.Any());
        Assert.IsType<List<DoctorSpecialty>>(result);
        Assert.Equal(3, result.Count);
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
