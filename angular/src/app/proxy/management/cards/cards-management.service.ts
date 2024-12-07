import type { CardDeletionApprovalInput, CardManagementOutputDto, GetCardManagementInput } from './dto/models';
import { RestService, Rest } from '@abp/ng.core';
import type { PagedResultDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';
import type { AddCardInput } from '../../cards/dto/models';
import type { ResponseModel } from '../../models/models';

@Injectable({
  providedIn: 'root',
})
export class CardsManagementService {
  apiName = 'Default';
  

  addCard = (input: AddCardInput, config?: Partial<Rest.Config>) =>
    this.restService.request<any, ResponseModel>({
      method: 'POST',
      url: '/api/app/cards-management/card',
      body: input,
    },
    { apiName: this.apiName,...config });
  

  cardDeletionApprovalByIdAndInput = (id: string, input: CardDeletionApprovalInput, config?: Partial<Rest.Config>) =>
    this.restService.request<any, ResponseModel>({
      method: 'POST',
      url: `/api/app/cards-management/${id}/card-deletion-approval`,
      body: input,
    },
    { apiName: this.apiName,...config });
  

  getList = (input: GetCardManagementInput, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PagedResultDto<CardManagementOutputDto>>({
      method: 'GET',
      url: '/api/app/cards-management',
      params: { ownerIds: input.ownerIds, filter: input.filter, suppliers: input.suppliers, binIds: input.binIds, statuses: input.statuses, balanceFrom: input.balanceFrom, balanceTo: input.balanceTo, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName,...config });

  constructor(private restService: RestService) {}
}
