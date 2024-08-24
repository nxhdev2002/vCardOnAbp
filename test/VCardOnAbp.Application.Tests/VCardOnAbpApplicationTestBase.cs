using Volo.Abp.Modularity;

namespace VCardOnAbp;

public abstract class VCardOnAbpApplicationTestBase<TStartupModule> : VCardOnAbpTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
