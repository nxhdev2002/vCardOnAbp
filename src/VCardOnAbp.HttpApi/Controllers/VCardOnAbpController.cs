using VCardOnAbp.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace VCardOnAbp.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class VCardOnAbpController : AbpControllerBase
{
    protected VCardOnAbpController()
    {
        LocalizationResource = typeof(VCardOnAbpResource);
    }
}
