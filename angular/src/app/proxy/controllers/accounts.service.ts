import { RestService, Rest } from '@abp/ng.core';
import type { PagedResultDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';
import type { GetUserTransactionInput, UserTransactionDto } from '../accounts/dtos/models';

@Injectable({
  providedIn: 'root',
})
export class AccountsService {
  apiName = 'Default';
  

  getTransactionsByInput = (input: GetUserTransactionInput, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PagedResultDto<UserTransactionDto>>({
      method: 'GET',
      url: '/api/app/accounts/transactions',
      params: { filter: input.filter, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName,...config });

  constructor(private restService: RestService) {}
}
