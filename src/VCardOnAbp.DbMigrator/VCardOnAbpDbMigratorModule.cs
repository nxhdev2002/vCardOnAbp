using VCardOnAbp.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace VCardOnAbp.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(VCardOnAbpEntityFrameworkCoreModule),
    typeof(VCardOnAbpApplicationContractsModule)
)]
public class VCardOnAbpDbMigratorModule : AbpModule
{
}
