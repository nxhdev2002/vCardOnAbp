import type { PagedResultRequestDto } from '@abp/ng.core';
import type { UserTransactionType } from '../../transactions/user-transaction-type.enum';

export interface GetUserTransactionInput extends PagedResultRequestDto {
  filter?: string;
}

export interface ProfileInfoDto {
  username?: string;
  email?: string;
  isUnverifiedEmail: boolean;
  isUnset2FA: boolean;
  balance: number;
  roleName?: string;
}

export interface UserTransactionDto {
  creationTime?: string;
  userId?: string;
  relatedEntity?: string;
  description?: string;
  amount: number;
  type: UserTransactionType;
}
