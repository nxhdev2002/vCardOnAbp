import type { BinDto, CreateBinDtoInput, GetBinDtoInput, UpdateBinDtoInput } from './dtos/models';
import { RestService, Rest } from '@abp/ng.core';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class BinCardService {
  apiName = 'Default';
  

  create = (input: CreateBinDtoInput, config?: Partial<Rest.Config>) =>
    this.restService.request<any, BinDto>({
      method: 'POST',
      url: '/api/app/bin-card',
      body: input,
    },
    { apiName: this.apiName,...config });
  

  delete = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: `/api/app/bin-card/${id}`,
    },
    { apiName: this.apiName,...config });
  

  get = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, BinDto>({
      method: 'GET',
      url: `/api/app/bin-card/${id}`,
    },
    { apiName: this.apiName,...config });
  

  getList = (input: GetBinDtoInput, config?: Partial<Rest.Config>) =>
    this.restService.request<any, BinDto[]>({
      method: 'GET',
      url: '/api/app/bin-card',
      params: { filter: input.filter, sorting: input.sorting, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName,...config });
  

  update = (id: string, input: UpdateBinDtoInput, config?: Partial<Rest.Config>) =>
    this.restService.request<any, BinDto>({
      method: 'PUT',
      url: `/api/app/bin-card/${id}`,
      body: input,
    },
    { apiName: this.apiName,...config });

  constructor(private restService: RestService) {}
}
