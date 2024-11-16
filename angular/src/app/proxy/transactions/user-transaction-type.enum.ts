import { mapEnumToOptions } from '@abp/ng.core';

export enum UserTransactionType {
  CreateCard = 0,
  FundCard = 1,
}

export const userTransactionTypeOptions = mapEnumToOptions(UserTransactionType);
