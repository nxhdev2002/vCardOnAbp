namespace VCardOnAbp.Permissions;

public static class VCardOnAbpPermissions
{
    public const string CardGroup = "Card";


    //Add your own permission names. Example:
    //public const string MyPermission1 = GroupName + ".MyPermission1";
    public const string ViewCard = CardGroup + ".View";
    public const string ViewCardTransaction = CardGroup + ".ViewTransaction";
    public const string CreateCard = CardGroup + ".Create";
    public const string FundCard = CardGroup + ".Fund";
    public const string DeleteCard = CardGroup + ".Delete";


    // BIN
    public const string BinGroup = "Bin";


    //Add your own permission names. Example:
    //public const string MyPermission1 = GroupName + ".MyPermission1";
    public const string ViewBin = BinGroup + ".View";
    public const string AddBin = BinGroup + ".Add";
    public const string EditBin = BinGroup + ".Edit";


    // Currency
    public const string CurrencyGroup = "Currency";
    public const string ViewCurrency = CurrencyGroup + ".View";
    public const string AddCurrency = CurrencyGroup + ".Add";
    public const string EditCurrency = CurrencyGroup + ".Edit";


    // Payment
    public const string PaymentGroup = "Payment";
    public const string ViewPayment = PaymentGroup + ".View";
    public const string AddPayment = PaymentGroup + ".Add";
    public const string EditPayment = PaymentGroup + ".Edit";
    public const string Deposit = PaymentGroup + ".Deposit";
    public const string ProcessDeposit = PaymentGroup + ".ProcessDeposit";
    public const string ViewDepositTransaction = PaymentGroup + ".ViewDepositTransaction";
}
