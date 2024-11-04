import type { PagedAndSortedResultRequestDto } from '@abp/ng.core';
import type { GatewayType } from '../gateway-type.enum';
import type { DepositTransactionStatus } from '../deposit-transaction-status.enum';

export interface CreateDepositTransactionDto {
  id?: string;
}

export interface CreateDepositTransactionInput {
  signature?: string;
  amount: number;
}

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
  gatewayType: GatewayType;
}

export interface ProcessTransactionInput {
  status: DepositTransactionStatus;
  comment?: string;
  concurrencyStamp?: string;
}
