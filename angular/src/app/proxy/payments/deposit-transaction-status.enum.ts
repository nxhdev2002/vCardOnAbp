import { mapEnumToOptions } from '@abp/ng.core';

export enum DepositTransactionStatus {
  Pending = 0,
  Completed = 1,
  Failed = 2,
}

export const depositTransactionStatusOptions = mapEnumToOptions(DepositTransactionStatus);
