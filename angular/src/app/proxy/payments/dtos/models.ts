import type { PagedAndSortedResultRequestDto } from '@abp/ng.core';

export interface GetPaymentMethodsInput extends PagedAndSortedResultRequestDto {
  filter?: string;
}

export interface PaymentMethodDto {
  id: number;
  name?: string;
  description?: string;
  isEnabled: boolean;
  fixedFee: number;
  percentageFee: number;
}
