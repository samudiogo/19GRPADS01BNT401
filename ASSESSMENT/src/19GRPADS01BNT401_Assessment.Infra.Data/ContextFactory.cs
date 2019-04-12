using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace _19GRPADS01BNT401_Assessment.Infra.Data
{
    public class ContextFactory: IDesignTimeDbContextFactory<AssessmentDbContext>
    {
        public AssessmentDbContext CreateDbContext()
        {
            return CreateDbContext(null);
        }

        public AssessmentDbContext CreateDbContext(string[] args)
        {
            var builderConfiguration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");
            var configuration = builderConfiguration.Build();
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            var builder = new DbContextOptionsBuilder<AssessmentDbContext>();
            builder.UseSqlServer(connectionString);

            return new AssessmentDbContext(builder.Options);
        }
    }
}