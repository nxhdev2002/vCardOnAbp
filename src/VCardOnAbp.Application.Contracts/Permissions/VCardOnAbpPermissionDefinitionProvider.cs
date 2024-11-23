using VCardOnAbp.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace VCardOnAbp.Permissions;

public class VCardOnAbpPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        PermissionGroupDefinition cardGroup = context.AddGroup(VCardOnAbpPermissions.CardGroup);

        //Define your own permissions here. Example:
        //myGroup.AddPermission(VCardOnAbpPermissions.MyPermission1, L("Permission:MyPermission1"));
        cardGroup.AddPermission(VCardOnAbpPermissions.CardGroup, L("CardPermission:Card"));
        cardGroup.AddPermission(VCardOnAbpPermissions.ViewCard, L("CardPermission:View"));
        cardGroup.AddPermission(VCardOnAbpPermissions.ViewCardTransaction, L("CardPermission:ViewTransaction"));
        cardGroup.AddPermission(VCardOnAbpPermissions.CreateCard, L("CardPermission:Create"));
        cardGroup.AddPermission(VCardOnAbpPermissions.FundCard, L("CardPermission:Fund"));
        cardGroup.AddPermission(VCardOnAbpPermissions.DeleteCard, L("CardPermission:Delete"));


        // BIN
        PermissionGroupDefinition binGroup = context.AddGroup(VCardOnAbpPermissions.BinGroup);
        binGroup.AddPermission(VCardOnAbpPermissions.BinGroup, L("BinPermission:Bin"));
        binGroup.AddPermission(VCardOnAbpPermissions.ViewBin, L("BinPermission:View"));
        binGroup.AddPermission(VCardOnAbpPermissions.AddBin, L("BinPermission:Add"));
        binGroup.AddPermission(VCardOnAbpPermissions.EditBin, L("BinPermission:Edit"));


        // Currency
        PermissionGroupDefinition currencyGroup = context.AddGroup(VCardOnAbpPermissions.CurrencyGroup);
        currencyGroup.AddPermission(VCardOnAbpPermissions.CurrencyGroup, L("CurrencyPermission:Currency"));
        currencyGroup.AddPermission(VCardOnAbpPermissions.ViewCurrency, L("CurrencyPermission:View"));
        currencyGroup.AddPermission(VCardOnAbpPermissions.AddCurrency, L("CurrencyPermission:Add"));
        currencyGroup.AddPermission(VCardOnAbpPermissions.EditCurrency, L("CurrencyPermission:Edit"));

        // Payment
        PermissionGroupDefinition paymentGroup = context.AddGroup(VCardOnAbpPermissions.PaymentGroup);
        paymentGroup.AddPermission(VCardOnAbpPermissions.PaymentGroup, L("PaymentPermission:Payment"));
        paymentGroup.AddPermission(VCardOnAbpPermissions.AddPayment, L("PaymentPermission:View"));
        paymentGroup.AddPermission(VCardOnAbpPermissions.ViewPayment, L("PaymentPermission:Add"));
        paymentGroup.AddPermission(VCardOnAbpPermissions.EditPayment, L("PaymentPermission:Edit"));
        paymentGroup.AddPermission(VCardOnAbpPermissions.Deposit, L("PaymentPermission:Deposit"));
        paymentGroup.AddPermission(VCardOnAbpPermissions.ProcessDeposit, L("PaymentPermission:Process"));
        paymentGroup.AddPermission(VCardOnAbpPermissions.ViewDepositTransaction, L("PaymentPermission:ViewDepositTransaction"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<VCardOnAbpResource>(name);
    }
}
