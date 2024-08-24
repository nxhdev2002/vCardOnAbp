using Volo.Abp.Modularity;

namespace VCardOnAbp;

[DependsOn(
    typeof(VCardOnAbpDomainModule),
    typeof(VCardOnAbpTestBaseModule)
)]
public class VCardOnAbpDomainTestModule : AbpModule
{

}
