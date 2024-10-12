using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace VCardOnAbp.EntityFrameworkCore;

/* This class is needed for EF Core console commands
 * (like Add-Migration and Update-Database commands) */
public class VCardOnAbpDbContextFactory : IDesignTimeDbContextFactory<VCardOnAbpDbContext>
{
    public VCardOnAbpDbContext CreateDbContext(string[] args)
    {
        IConfigurationRoot configuration = BuildConfiguration();

        VCardOnAbpEfCoreEntityExtensionMappings.Configure();

        DbContextOptionsBuilder<VCardOnAbpDbContext> builder = new DbContextOptionsBuilder<VCardOnAbpDbContext>()
            .UseSqlServer(configuration.GetConnectionString("Default"));

        return new VCardOnAbpDbContext(builder.Options);
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        IConfigurationBuilder builder = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../VCardOnAbp.DbMigrator/"))
            .AddJsonFile("appsettings.json", optional: false);

        return builder.Build();
    }
}
