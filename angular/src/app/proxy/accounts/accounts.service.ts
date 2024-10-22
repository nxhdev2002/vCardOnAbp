import { RestService, Rest } from '@abp/ng.core';
import { Injectable } from '@angular/core';
import type { ChangePasswordInput, ProfileDto, UpdateProfileDto } from '../volo/abp/account/models';

@Injectable({
  providedIn: 'root',
})
export class AccountsService {
  apiName = 'Default';
  

  changePassword = (input: ChangePasswordInput, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'POST',
      url: '/api/app/accounts/change-password',
      body: input,
    },
    { apiName: this.apiName,...config });
  

  get = (config?: Partial<Rest.Config>) =>
    this.restService.request<any, ProfileDto>({
      method: 'GET',
      url: '/api/app/accounts',
    },
    { apiName: this.apiName,...config });
  

  update = (input: UpdateProfileDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, ProfileDto>({
      method: 'PUT',
      url: '/api/app/accounts',
      body: input,
    },
    { apiName: this.apiName,...config });

  constructor(private restService: RestService) {}
}
