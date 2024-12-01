import { mapEnumToOptions } from '@abp/ng.core';

export enum CardStatus {
  Pending = 0,
  Active = 1,
  Inactive = 2,
  Lock = 3,
  PendingDelete = 4,
}

export const cardStatusOptions = mapEnumToOptions(CardStatus);
