import type { Supplier } from '../../cards/supplier.enum';
import type { PagedAndSortedResultRequestDto } from '@abp/ng.core';

export interface BinDto {
  id?: string;
  name?: string;
  description?: string;
}

export interface CreateBinDtoInput {
  name?: string;
  description?: string;
  supplier: Supplier;
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
