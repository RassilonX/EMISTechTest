using DAL;
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
