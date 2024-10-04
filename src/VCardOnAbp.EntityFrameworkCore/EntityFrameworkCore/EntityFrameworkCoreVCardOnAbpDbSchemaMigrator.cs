using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using VCardOnAbp.Data;
using Volo.Abp.DependencyInjection;

namespace VCardOnAbp.EntityFrameworkCore;

public class EntityFrameworkCoreVCardOnAbpDbSchemaMigrator
    : IVCardOnAbpDbSchemaMigrator, ITransientDependency
{
    private readonly IServiceProvider _serviceProvider;

    public EntityFrameworkCoreVCardOnAbpDbSchemaMigrator(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task MigrateAsync()
    {
        /* We intentionally resolving the VCardOnAbpDbContext
         * from IServiceProvider (instead of directly injecting it)
         * to properly get the connection string of the current tenant in the
         * current scope.
         */

        await _serviceProvider
            .GetRequiredService<VCardOnAbpDbContext>()
            .Database
            .MigrateAsync();
    }
}
