using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace VCardOnAbp.EntityFrameworkCore;

/* This class is needed for EF Core console commands
 * (like Add-Migration and Update-Database commands) */
public class VCardOnAbpDbContextFactory : IDesignTimeDbContextFactory<VCardOnAbpDbContext>
{
    public VCardOnAbpDbContext CreateDbContext(string[] args)
    {
        var configuration = BuildConfiguration();
        
        VCardOnAbpEfCoreEntityExtensionMappings.Configure();

        var builder = new DbContextOptionsBuilder<VCardOnAbpDbContext>()
            .UseSqlServer(configuration.GetConnectionString("Default"));
        
        return new VCardOnAbpDbContext(builder.Options);
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../VCardOnAbp.DbMigrator/"))
            .AddJsonFile("appsettings.json", optional: false);

        return builder.Build();
    }
}
