import { mapEnumToOptions } from '@abp/ng.core';

export enum UserTransactionType {
  CreateCard = 0,
  FundCard = 1,
  Deposit = 2,
}

export const userTransactionTypeOptions = mapEnumToOptions(UserTransactionType);
