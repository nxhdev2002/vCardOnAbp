import type { EntityDto } from '@abp/ng.core';

export interface CreateCurrencyDto {
  name?: string;
  code?: string;
  symbol?: string;
  usdRate?: number;
}

export interface CurrencyDto extends EntityDto<string> {
  name?: string;
  code?: string;
  symbol?: string;
  usdRate?: number;
}

export interface UpdateCurrencyDto extends CreateCurrencyDto {
  id?: string;
}
