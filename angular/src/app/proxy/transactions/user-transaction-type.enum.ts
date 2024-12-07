import { mapEnumToOptions } from '@abp/ng.core';

export enum UserTransactionType {
  CreateCard = 0,
  FundCard = 1,
  Deposit = 2,
  RefundAmount = 3,
}

export const userTransactionTypeOptions = mapEnumToOptions(UserTransactionType);
