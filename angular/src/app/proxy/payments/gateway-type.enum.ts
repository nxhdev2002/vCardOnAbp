import { mapEnumToOptions } from '@abp/ng.core';

export enum GatewayType {
  MANUAL = 0,
  AUTOBANK = 1,
}

export const gatewayTypeOptions = mapEnumToOptions(GatewayType);
