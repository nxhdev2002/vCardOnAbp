using Volo.Abp.Settings;

namespace VCardOnAbp.Settings;

public class VCardOnAbpSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        //Define your own settings here. Example:
        //context.Add(new SettingDefinition(VCardOnAbpSettings.MySetting1));
    }
}
