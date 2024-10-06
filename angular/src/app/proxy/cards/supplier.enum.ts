import { mapEnumToOptions } from '@abp/ng.core';

export enum Supplier {
  Vmcardio = 0,
}

export const supplierOptions = mapEnumToOptions(Supplier);
