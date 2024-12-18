import type { CardDto, CardSecretDto, CardTransactionDto, CreateCardInput, FundCardInput, GetCardInput, GetCardTransactionInput, NoteCardInput } from './dto/models';
import { RestService, Rest } from '@abp/ng.core';
import type { PagedResultDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';
import type { ResponseModel } from '../models/models';

@Injectable({
  providedIn: 'root',
})
export class CardsService {
  apiName = 'Default';
  

  cancelDeleteById = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, ResponseModel>({
      method: 'POST',
      url: `/api/app/cards/${id}/cancel-delete`,
    },
    { apiName: this.apiName,...config });
  

  create = (input: CreateCardInput, config?: Partial<Rest.Config>) =>
    this.restService.request<any, ResponseModel>({
      method: 'POST',
      url: '/api/app/cards',
      body: input,
    },
    { apiName: this.apiName,...config });
  

  delete = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, ResponseModel>({
      method: 'DELETE',
      url: `/api/app/cards/${id}`,
    },
    { apiName: this.apiName,...config });
  

  fund = (id: string, input: FundCardInput, config?: Partial<Rest.Config>) =>
    this.restService.request<any, ResponseModel>({
      method: 'POST',
      url: `/api/app/cards/${id}/fund`,
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
      params: { filter: input.filter, suppliers: input.suppliers, binIds: input.binIds, statuses: input.statuses, balanceFrom: input.balanceFrom, balanceTo: input.balanceTo, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName,...config });
  

  getSecret = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, CardSecretDto>({
      method: 'GET',
      url: `/api/app/cards/${id}/secret`,
    },
    { apiName: this.apiName,...config });
  

  getTransaction = (id: string, input: GetCardTransactionInput, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PagedResultDto<CardTransactionDto>>({
      method: 'GET',
      url: `/api/app/cards/${id}/transaction`,
      params: { filter: input.filter, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName,...config });
  

  noteByIdAndInput = (id: string, input: NoteCardInput, config?: Partial<Rest.Config>) =>
    this.restService.request<any, ResponseModel>({
      method: 'POST',
      url: `/api/app/cards/${id}/note`,
      body: input,
    },
    { apiName: this.apiName,...config });
  

  refreshCardById = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, ResponseModel>({
      method: 'POST',
      url: `/api/app/cards/${id}/refresh-card`,
    },
    { apiName: this.apiName,...config });

  constructor(private restService: RestService) {}
}
