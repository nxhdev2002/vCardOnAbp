using Volo.Abp.Modularity;

namespace VCardOnAbp;

/* Inherit from this class for your domain layer tests. */
public abstract class VCardOnAbpDomainTestBase<TStartupModule> : VCardOnAbpTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
