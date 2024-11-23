import type { CreateDepositTransactionDto, CreateDepositTransactionInput, DepositTransactionDto, GetDepositTransactionInput, GetPaymentMethodsInput, PaymentMethodDto, ProcessTransactionInput } from './dtos/models';
import { RestService, Rest } from '@abp/ng.core';
import type { PagedResultDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class PaymentsService {
  apiName = 'Default';
  

  createTransactionByIdAndInput = (id: number, input: CreateDepositTransactionInput, config?: Partial<Rest.Config>) =>
    this.restService.request<any, CreateDepositTransactionDto>({
      method: 'POST',
      url: `/api/app/payments/${id}/transaction`,
      body: input,
    },
    { apiName: this.apiName,...config });
  

  getDepositTransactionsByInput = (input: GetDepositTransactionInput, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PagedResultDto<DepositTransactionDto>>({
      method: 'GET',
      url: '/api/app/payments/deposit-transactions',
      params: { filter: input.filter, startDate: input.startDate, endDate: input.endDate, sorting: input.sorting, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName,...config });
  

  getPaymentMethods = (input: GetPaymentMethodsInput, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PagedResultDto<PaymentMethodDto>>({
      method: 'GET',
      url: '/api/app/payments/payment-methods',
      params: { filter: input.filter, sorting: input.sorting, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName,...config });
  

  updateTransactionByIdAndTransIdAndInput = (id: number, transId: string, input: ProcessTransactionInput, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'PUT',
      url: `/api/app/payments/${id}/transaction/${transId}`,
      body: input,
    },
    { apiName: this.apiName,...config });

  constructor(private restService: RestService) {}
}
