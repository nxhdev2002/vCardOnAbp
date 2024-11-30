import { RestService, Rest } from '@abp/ng.core';
import type { PagedResultDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';
import type { GetUserTransactionInput, ProfileInfoDto, UserTransactionDto } from '../accounts/dtos/models';

@Injectable({
  providedIn: 'root',
})
export class AccountsService {
  apiName = 'Default';
  

  getProfile = (config?: Partial<Rest.Config>) =>
    this.restService.request<any, ProfileInfoDto>({
      method: 'GET',
      url: '/api/app/account/profile',
    },
    { apiName: this.apiName,...config });
  

  getTransactionsByInput = (input: GetUserTransactionInput, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PagedResultDto<UserTransactionDto>>({
      method: 'GET',
      url: '/api/app/accounts/transactions',
      params: { filter: input.filter, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName,...config });

  constructor(private restService: RestService) {}
}
