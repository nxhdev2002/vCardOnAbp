namespace VCardOnAbp.Payments.Dtos;
public record ProcessTransactionInput(DepositTransactionStatus Status, string Comment, string ConcurrencyStamp);
