
export interface GetCardInput {
  bin?: string;
  card_id?: string;
  uid?: string;
}

export interface GetCardsFilterInput {
  card_number?: string;
  card_name_new?: string;
  card_no?: string;
  alias?: string;
  status?: string;
  bin?: string;
  page?: string;
  page_size?: string;
  type?: string;
  uid?: string;
}

export interface VmCardDto {
  id: number;
  uid: number;
  user_name?: string;
  card_name?: string;
  card_use_name?: string;
  tag?: string;
  bin?: string;
  card_id?: string;
  card_number?: string;
  card_no?: string;
  trans_merchant?: string;
  encrypted_cvv?: string;
  encrypted_expiration?: string;
  last_four?: string;
  skip_reconcil?: number;
  alias?: string;
  status?: string;
  available_amount?: number;
  visable?: number;
  status_remark?: string;
  create_time?: string;
  update_time?: string;
  frozen_time?: string;
  frozen_amount?: number;
  bin_num?: string;
  due_days?: number;
}
