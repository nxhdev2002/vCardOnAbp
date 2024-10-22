import type { Supplier } from '../../cards/supplier.enum';
import type { PagedAndSortedResultRequestDto } from '@abp/ng.core';

export interface BinDto {
  id?: string;
  name?: string;
  description?: string;
  currency?: string;
  symbol?: string;
  creationFixedFee: number;
  creationPercentFee: number;
  fundingFixedFee: number;
  fundingPercentFee: number;
}

export interface CreateBinDtoInput {
  name?: string;
  description?: string;
  supplier: Supplier;
  currencyId?: string;
  supplierMapping?: string;
  creationFixedFee: number;
  creationPercentFee: number;
  fundingFixedFee: number;
  fundingPercentFee: number;
}

export interface GetBinDtoInput extends PagedAndSortedResultRequestDto {
  filter?: string;
}

export interface UpdateBinDtoInput {
  creationFixedFee?: number;
  creationPercentFee?: number;
  fundingFixedFee?: number;
  fundingPercentFee?: number;
}
