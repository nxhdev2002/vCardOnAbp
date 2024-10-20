import type { AddCardInput, CardDto, CardTransactionDto, CreateCardInput, FundCardInput, GetCardInput, GetCardTransactionInput } from './dto/models';
import { RestService, Rest } from '@abp/ng.core';
import type { PagedResultDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class CardsService {
  apiName = 'Default';
  

  addCardByInput = (input: AddCardInput, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'POST',
      url: '/api/app/cards/card',
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
  

  delete = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: `/api/app/cards/${id}`,
    },
    { apiName: this.apiName,...config });
  

  fund = (input: FundCardInput, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'POST',
      url: '/api/app/cards/fund',
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
    this.restService.request<any, PagedResultDto<CardDto>>({
      method: 'GET',
      url: '/api/app/cards',
      params: { filter: input.filter, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName,...config });
  

  getTransaction = (input: GetCardTransactionInput, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PagedResultDto<CardTransactionDto>>({
      method: 'GET',
      url: '/api/app/cards/transaction',
      params: { cardId: input.cardId, filter: input.filter, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName,...config });

  constructor(private restService: RestService) {}
}
