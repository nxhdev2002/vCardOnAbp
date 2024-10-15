namespace VCardOnAbp.Permissions;

public static class VCardOnAbpPermissions
{
    public const string CardGroup = "Card";


    //Add your own permission names. Example:
    //public const string MyPermission1 = GroupName + ".MyPermission1";
    public const string ViewCard = CardGroup + ".ViewCard";
    public const string AddCard = CardGroup + ".AddCard";
    public const string ViewCardTransaction = CardGroup + ".ViewCardTransaction";
    public const string CreateCard = CardGroup + ".CreateCard";
    public const string FundCard = CardGroup + ".FundCard";


    // BIN
    public const string BinGroup = "Bin";


    //Add your own permission names. Example:
    //public const string MyPermission1 = GroupName + ".MyPermission1";
    public const string ViewBin = BinGroup + ".ViewBin";
    public const string AddBin = BinGroup + ".AddBin";
    public const string EditBin = BinGroup + ".EditBin";
}
