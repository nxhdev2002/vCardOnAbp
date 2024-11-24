import type { CreateDepositTransactionDto, CreateDepositTransactionInput, DepositTransactionDto, GetDepositTransactionInput, GetPaymentMethodsInput, PaymentMethodDto } from './dtos/models';
import { RestService, Rest } from '@abp/ng.core';
import type { PagedResultDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';
import type { ResponseModel } from '../models/models';

@Injectable({
  providedIn: 'root',
})
export class PaymentsService {
  apiName = 'Default';
  

  approveTransactionById = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, ResponseModel>({
      method: 'POST',
      url: `/api/app/payments/${id}/approve-transaction`,
    },
    { apiName: this.apiName,...config });
  

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
      params: { filter: input.filter, startDate: input.startDate, endDate: input.endDate, status: input.status, sorting: input.sorting, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName,...config });
  

  getPaymentMethods = (input: GetPaymentMethodsInput, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PagedResultDto<PaymentMethodDto>>({
      method: 'GET',
      url: '/api/app/payments/payment-methods',
      params: { filter: input.filter, sorting: input.sorting, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName,...config });
  

  getPendingTransactionsByInput = (input: GetDepositTransactionInput, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PagedResultDto<DepositTransactionDto>>({
      method: 'GET',
      url: '/api/app/payments/pending-transactions',
      params: { filter: input.filter, startDate: input.startDate, endDate: input.endDate, status: input.status, sorting: input.sorting, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName,...config });
  

  rejectTransactionById = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, ResponseModel>({
      method: 'POST',
      url: `/api/app/payments/${id}/reject-transaction`,
    },
    { apiName: this.apiName,...config });

  constructor(private restService: RestService) {}
}
