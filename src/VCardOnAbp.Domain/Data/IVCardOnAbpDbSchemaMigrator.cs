using System.Threading.Tasks;

namespace VCardOnAbp.Data;

public interface IVCardOnAbpDbSchemaMigrator
{
    Task MigrateAsync();
}
