import { mapEnumToOptions } from '@abp/ng.core';

export enum GatewayRowActions {
  Deposit = 0,
  Delete = 1,
  Edit = 2,
}

export const gatewayRowActionsOptions = mapEnumToOptions(GatewayRowActions);
