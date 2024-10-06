import { mapEnumToOptions } from '@abp/ng.core';

export enum CardAction {
  LOCK = 0,
  DELETE = 1,
  FUND = 2,
  VIEW = 3,
}

export const cardActionOptions = mapEnumToOptions(CardAction);
