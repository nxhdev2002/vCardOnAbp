using Volo.Abp.Modularity;

namespace VCardOnAbp;

[DependsOn(
    typeof(VCardOnAbpApplicationModule),
    typeof(VCardOnAbpDomainTestModule)
)]
public class VCardOnAbpApplicationTestModule : AbpModule
{

}
