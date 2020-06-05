using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;
namespace finalb2020.DataContext
{
    public class FinalDbContextFactory : IDesignTimeDbContextFactory<FinalDbContext>
    {
        public FinalDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();
            var optionBuilder = new DbContextOptionsBuilder<FinalDbContext>();
            optionBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            return new FinalDbContext(optionBuilder.Options);
        }
    }
}