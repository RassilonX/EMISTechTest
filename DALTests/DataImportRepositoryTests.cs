using DAL;
using Database;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace DALTests;

public class DataImportRepositoryTests
{
    private DataImportRepository repo { get; set; }
    private readonly Mock<DatabaseDbContext> _dbContext;

    public DataImportRepositoryTests()
    {
        var dbContextOptions = new DbContextOptions<DatabaseDbContext>();
        _dbContext = new Mock<DatabaseDbContext>(dbContextOptions);
        repo = new DataImportRepository(_dbContext.Object);
    }

    [Theory]
    [InlineData("hello-world!", "Hello-World")]
    [InlineData("hello@world!", "Helloworld")]
    [InlineData("hello world", "Hello World")]
    [InlineData("hëllo wørld", "Hëllo Wørld")]
    [InlineData("hello-world", "Hello-World")]
    public void SanitiseStringInput_ReturnsExpectedSanitizedString(string input, string expectedOutput)
    {
        // Arrange - Act
        var result = repo.SanitiseStringInput(input);

        // Assert
        Assert.Equal(expectedOutput, result);
    }
}