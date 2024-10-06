import type { ActionInput, CardDto, CreateCardInput, GetCardInput } from './dto/models';
import { RestService, Rest } from '@abp/ng.core';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class CardsService {
  apiName = 'Default';
  

  actionByInput = (input: ActionInput, config?: Partial<Rest.Config>) =>
    this.restService.request<any, object>({
      method: 'POST',
      url: '/api/app/cards/action',
      body: input,
    },
    { apiName: this.apiName,...config });
  

  create = (input: CreateCardInput, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'POST',
      url: '/api/app/cards',
      body: input,
    },
    { apiName: this.apiName,...config });
  

  get = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, CardDto>({
      method: 'GET',
      url: `/api/app/cards/${id}`,
    },
    { apiName: this.apiName,...config });
  

  getList = (input: GetCardInput, config?: Partial<Rest.Config>) =>
    this.restService.request<any, CardDto[]>({
      method: 'GET',
      url: '/api/app/cards',
      params: { filter: input.filter, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName,...config });

  constructor(private restService: RestService) {}
}
