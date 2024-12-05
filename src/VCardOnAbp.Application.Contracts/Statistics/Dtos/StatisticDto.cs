using System;
using System.Collections.Generic;
using VCardOnAbp.Cards;

namespace VCardOnAbp.Statistics.Dtos;
public record StatisticDto(
    Guid UserId,
    long TotalCardCount, 
    long TotalTransaction, 
    List<FailedTransactionRate> FailedTransactionRates
);

public record FailedTransactionRate(
    Guid Owner,
    string BinName,
    Supplier Supplier,
    long FailCount,
    long TotalCount,
    double Rate
);