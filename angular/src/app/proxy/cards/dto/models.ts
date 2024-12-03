import type { EntityDto, PagedResultRequestDto } from '@abp/ng.core';
import type { CardStatus } from '../card-status.enum';

export interface CardDto extends EntityDto<string> {
  cardNo?: string;
  balance: number;
  cardStatus: CardStatus;
  cardName?: string;
  binId?: string;
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
  cardName?: string;
  binId?: string;
  note?: string;
}

export interface FundCardInput {
  amount: number;
}

export interface GetCardInput extends PagedResultRequestDto {
  filter?: string;
}

export interface GetCardTransactionInput extends PagedResultRequestDto {
  filter?: string;
}
