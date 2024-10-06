import type { EntityDto, PagedResultRequestDto } from '@abp/ng.core';
import type { CardAction } from '../card-action.enum';
import type { CardStatus } from '../card-status.enum';
import type { Supplier } from '../supplier.enum';

export interface ActionInput extends EntityDto<string> {
  action: CardAction;
  value?: number;
  dataHash?: string;
}

export interface CardDto extends EntityDto<string> {
  cardNo?: string;
  balance: number;
  cardStatus: CardStatus;
}

export interface CreateCardInput {
  amount: number;
  supplier: Supplier;
  cardName?: string;
}

export interface GetCardInput extends PagedResultRequestDto {
  filter?: string;
}
