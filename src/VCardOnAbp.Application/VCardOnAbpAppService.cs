using VCardOnAbp.Localization;
using Volo.Abp.Application.Services;

namespace VCardOnAbp;

/* Inherit your application services from this class.
 */
public abstract class VCardOnAbpAppService : ApplicationService
{
    protected VCardOnAbpAppService()
    {
        LocalizationResource = typeof(VCardOnAbpResource);
    }
}
