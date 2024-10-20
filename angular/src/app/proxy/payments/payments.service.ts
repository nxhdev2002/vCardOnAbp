import type { GetPaymentMethodsInput, PaymentMethodDto } from './dtos/models';
import { RestService, Rest } from '@abp/ng.core';
import type { PagedResultDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class PaymentsService {
  apiName = 'Default';
  

  getPaymentMethods = (input: GetPaymentMethodsInput, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PagedResultDto<PaymentMethodDto>>({
      method: 'GET',
      url: '/api/app/payments/payment-methods',
      params: { filter: input.filter, sorting: input.sorting, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName,...config });

  constructor(private restService: RestService) {}
}
