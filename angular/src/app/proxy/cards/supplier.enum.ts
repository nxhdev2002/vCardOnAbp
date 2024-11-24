import { mapEnumToOptions } from '@abp/ng.core';

export enum Supplier {
  Vmcardio = 0,
  Vcc51 = 1,
}

export const supplierOptions = mapEnumToOptions(Supplier);
