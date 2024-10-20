import type { CardStatus } from '../card-status.enum';
import type { Supplier } from '../supplier.enum';
import type { EntityDto, PagedResultRequestDto } from '@abp/ng.core';

export interface AddCardInput {
  userId?: string;
  cardNo?: string;
  supplierIdentity?: string;
  status: CardStatus;
  balance: number;
  supplier: Supplier;
}

export interface CardDto extends EntityDto<string> {
  cardNo?: string;
  balance: number;
  cardStatus: CardStatus;
}

export interface CardTransactionDto {
  cardId?: string;
  authAmount: number;
  currency?: string;
  description?: string;
  merchantName?: string;
  settleAmount?: number;
  status?: string;
  type?: string;
  creationTime?: string;
}

export interface CreateCardInput {
  amount: number;
  supplier: Supplier;
  cardName?: string;
}

export interface FundCardInput extends EntityDto<string> {
  amount: number;
}

export interface GetCardInput extends PagedResultRequestDto {
  filter?: string;
}

export interface GetCardTransactionInput extends PagedResultRequestDto {
  cardId?: string;
  filter?: string;
}
