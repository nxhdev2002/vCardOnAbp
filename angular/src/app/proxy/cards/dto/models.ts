import type { EntityDto, PagedResultRequestDto } from '@abp/ng.core';
import type { CardStatus } from '../card-status.enum';
import type { Supplier } from '../supplier.enum';

export interface CardDto extends EntityDto<string> {
  cardNo?: string;
  balance: number;
  cardStatus: CardStatus;
}

export interface CardSecretDto {
  cvv?: string;
  expirationTime?: string;
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
  filter?: string;
}
