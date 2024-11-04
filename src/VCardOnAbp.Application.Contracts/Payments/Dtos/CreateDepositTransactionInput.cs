using System;

namespace VCardOnAbp.Payments.Dtos;
public record CreateDepositTransactionInput(string Signature, decimal Amount);

public record CreateDepositTransactionDto(Guid Id);