import type { CardDto, GetCardInput } from '../../../cards/dto/models';

export interface CardDeletionApprovalInput {
  isApproved: boolean;
  refundAmount?: number;
}

export interface CardManagementOutputDto extends CardDto {
  ownerId?: string;
  ownerName?: string;
}

export interface GetCardManagementInput extends GetCardInput {
  ownerIds: string[];
}
