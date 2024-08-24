using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace VCardOnAbp.Data;

/* This is used if database provider does't define
 * IVCardOnAbpDbSchemaMigrator implementation.
 */
public class NullVCardOnAbpDbSchemaMigrator : IVCardOnAbpDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}
