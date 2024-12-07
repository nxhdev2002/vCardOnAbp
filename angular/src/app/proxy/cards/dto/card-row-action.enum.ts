import { mapEnumToOptions } from '@abp/ng.core';

export enum CardRowAction {
  View = 0,
  Delete = 1,
  Fund = 2,
  ApproveDelete = 3,
  RejectDelete = 4,
  Refresh = 5,
  Note = 6,
}

export const cardRowActionOptions = mapEnumToOptions(CardRowAction);
