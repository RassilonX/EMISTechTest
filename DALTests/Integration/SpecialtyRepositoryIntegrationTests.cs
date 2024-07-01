using DAL;
using Database;
using Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DALTests.Integration;

public class SpecialtyRepositoryIntegrationTests
{
    private readonly DatabaseDbContext _dbContext;
    private readonly SpecialtyRepository _repository;
    private readonly DataImportRepository _dataImportRepository;

    public SpecialtyRepositoryIntegrationTests()
    {
        var options = CreateOptions();

        _dbContext = new DatabaseDbContext(options);
        _repository = new SpecialtyRepository(_dbContext);
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
    public async Task DeleteAsync_ValidIdRemovesData()
    {
        // Arrange
        var specialty = new Specialty() { SpecialtyName = "Test Specialty" };
        _dbContext.Specialties.Add(specialty);
        _dbContext.SaveChanges();

        // Act
        await _repository.DeleteAsync(specialty.Id);

        var result = await _dbContext.Specialties.FindAsync(specialty.Id);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task SaveAsync_DataSavedToDatabase()
    {
        // Arrange
        var specialty = "Test Specialty";

        // Act
        await _repository.SaveAsync(specialty);

        var result = await _dbContext.Specialties.FirstOrDefaultAsync(s => s.SpecialtyName == specialty);

        _dbContext.Specialties.Remove(result);
        _dbContext.SaveChanges();

        // Assert
        Assert.NotNull(result);
        Assert.IsType<Specialty>(result);
        Assert.Equal(specialty, result.SpecialtyName);
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
