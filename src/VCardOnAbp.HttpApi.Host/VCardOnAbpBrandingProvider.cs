using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace VCardOnAbp;

[Dependency(ReplaceServices = true)]
public class VCardOnAbpBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "VCardOnAbp";
}
