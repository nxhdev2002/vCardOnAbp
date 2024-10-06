import type { GetCardInput, GetCardsFilterInput, VmCardDto } from './dtos/models';
import { RestService, Rest } from '@abp/ng.core';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class VmcardioService {
  apiName = 'Default';
  

  createCard = (config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'POST',
      url: '/api/app/vmcardio/card',
    },
    { apiName: this.apiName,...config });
  

  deleteCard = (config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: '/api/app/vmcardio/card',
    },
    { apiName: this.apiName,...config });
  

  getCardByInput = (input: GetCardInput, config?: Partial<Rest.Config>) =>
    this.restService.request<any, VmCardDto>({
      method: 'GET',
      url: '/api/app/vmcardio/card',
      params: { bin: input.bin, card_id: input.card_id, uid: input.uid },
    },
    { apiName: this.apiName,...config });
  

  getCardsByInput = (input: GetCardsFilterInput, config?: Partial<Rest.Config>) =>
    this.restService.request<any, object>({
      method: 'GET',
      url: '/api/app/vmcardio/cards',
      params: { card_number: input.card_number, card_name_new: input.card_name_new, card_no: input.card_no, alias: input.alias, status: input.status, bin: input.bin, page: input.page, page_size: input.page_size, type: input.type, uid: input.uid },
    },
    { apiName: this.apiName,...config });

  constructor(private restService: RestService) {}
}
