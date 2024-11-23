import type { DepositTransactionStatus } from '../deposit-transaction-status.enum';
import type { PagedAndSortedResultRequestDto } from '@abp/ng.core';
import type { GatewayType } from '../gateway-type.enum';

export interface CreateDepositTransactionDto {
  id?: string;
}

export interface CreateDepositTransactionInput {
  signature?: string;
  amount: number;
}

export interface DepositTransactionDto {
  paymentMethodId: number;
  amount: number;
  transactionStatus: DepositTransactionStatus;
  approvedAt?: string;
  comment?: string;
  requester?: string;
  creationTime?: string;
}

export interface GetDepositTransactionInput extends PagedAndSortedResultRequestDto {
  filter?: string;
  startDate?: string;
  endDate?: string;
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
  guideContent?: string;
  minAmount: number;
}

export interface ProcessTransactionInput {
  status: DepositTransactionStatus;
  comment?: string;
  concurrencyStamp?: string;
}
