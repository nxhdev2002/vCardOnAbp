import { mapEnumToOptions } from '@abp/ng.core';

export enum CardStatus {
  Active = 0,
  Inactive = 1,
  Lock = 2,
}

export const cardStatusOptions = mapEnumToOptions(CardStatus);
