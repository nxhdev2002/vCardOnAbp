using VCardOnAbp.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace VCardOnAbp.Permissions;

public class VCardOnAbpPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        PermissionGroupDefinition myGroup = context.AddGroup(VCardOnAbpPermissions.CardGroup);

        //Define your own permissions here. Example:
        //myGroup.AddPermission(VCardOnAbpPermissions.MyPermission1, L("Permission:MyPermission1"));

        myGroup.AddPermission(VCardOnAbpPermissions.ViewCard, L("CardPermission:View"));
        myGroup.AddPermission(VCardOnAbpPermissions.ViewCardTransaction, L("CardPermission:ViewTransaction"));
        myGroup.AddPermission(VCardOnAbpPermissions.CreateCard, L("CardPermission:Create"));
        myGroup.AddPermission(VCardOnAbpPermissions.AddCard, L("CardPermission:Add"));
        myGroup.AddPermission(VCardOnAbpPermissions.FundCard, L("CardPermission:Fund"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<VCardOnAbpResource>(name);
    }
}
