using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database;

public class DatabaseDbContextFactory : IDesignTimeDbContextFactory<DatabaseDbContext>
{
    public DatabaseDbContext CreateDbContext(string[] args)
    {
        var builder = new ConfigurationBuilder().AddJsonFile("appsettings.json");
        var configurationRoot = builder.Build();

        var connectionString = configurationRoot.GetSection("ConnectionStrings:sql_db").Value;

        var optionsBuilder = new DbContextOptionsBuilder<DatabaseDbContext>();
        optionsBuilder.UseSqlServer(connectionString);

        return new DatabaseDbContext(optionsBuilder.Options);
    }
}
