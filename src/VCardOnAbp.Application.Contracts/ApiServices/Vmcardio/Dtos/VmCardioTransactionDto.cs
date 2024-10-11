using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using VCardOnAbp.ApiServices.Vmcardio.Json;

namespace VCardOnAbp.ApiServices.Vmcardio.Dtos
{
    public class VmCardioTransactionResponse
    {
        public object card_fail_reason_list { get; set; }
        public List<VmCardioTransactionDto> list { get; set; }
        public int total { get; set; }
    }
    public class VmCardioTransactionDto
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }

        [JsonPropertyName("uid")]
        public int Uid { get; set; }

        [JsonPropertyName("bin")]
        public string? Bin { get; set; }

        [JsonPropertyName("card_number")]
        public string? CardNumber { get; set; }

        [JsonPropertyName("auth_id")]
        public string? AuthId { get; set; }

        [JsonPropertyName("match_id")]
        public string? MatchId { get; set; }

        [JsonPropertyName("settle_id")]
        public string? SettleId { get; set; }

        [JsonPropertyName("auth_time")]
        [JsonConverter(typeof(CustomNullableDateTimeConverter))]
        public DateTime? AuthTime { get; set; }

        [JsonPropertyName("auth_amount")]
        [JsonConverter(typeof(NullableDecimalConverter))]
        public decimal? AuthAmount { get; set; }

        [JsonPropertyName("auth_currency")]
        public string? AuthCurrency { get; set; }

        [JsonPropertyName("settle_time")]
        [JsonConverter(typeof(CustomNullableDateTimeConverter))]
        public DateTime? SettleTime { get; set; }

        [JsonPropertyName("settle_currency")]
        public string? SettleCurrency { get; set; }

        [JsonPropertyName("settle_amount")]
        [JsonConverter(typeof(NullableDecimalConverter))]
        public decimal? SettleAmount { get; set; }

        [JsonPropertyName("local_currency")]
        public string? LocalCurrency { get; set; }

        [JsonPropertyName("local_amount")]
        [JsonConverter(typeof(NullableDecimalConverter))]
        public decimal? LocalAmount { get; set; }

        [JsonPropertyName("status")]
        public string? Status { get; set; }

        [JsonPropertyName("type")]
        public string? Type { get; set; }

        [JsonPropertyName("description")]
        public string? Description { get; set; }

        [JsonPropertyName("merchant_name")]
        public string? MerchantName { get; set; }

        [JsonPropertyName("merchant_address")]
        public string? MerchantAddress { get; set; }

        [JsonPropertyName("merchant_code")]
        public string? MerchantCode { get; set; }

        [JsonPropertyName("remark")]
        public string? Remark { get; set; }

        [JsonPropertyName("visable")]
        public int? Visable { get; set; }

        [JsonPropertyName("display")]
        public int? Display { get; set; }

        [JsonPropertyName("update_time")]
        [JsonConverter(typeof(CustomNullableDateTimeConverter))]
        public DateTime? UpdateTime { get; set; }

        [JsonPropertyName("create_time")]
        [JsonConverter(typeof(CustomNullableDateTimeConverter))]
        public DateTime? CreateTime { get; set; }

        [JsonPropertyName("card_number_last_four")]
        public string? CardNumberLastFour { get; set; }

        [JsonPropertyName("is_mcc")]
        public int? IsMcc { get; set; }

        [JsonPropertyName("vm_fee")]
        public string? VmFee { get; set; }

        [JsonPropertyName("vm_id")]
        public string? VmId { get; set; }

        [JsonPropertyName("is_add_white_list")]
        public int? IsAddWhiteList { get; set; }

        [JsonPropertyName("delete_card_time")]
        [JsonConverter(typeof(CustomNullableDateTimeConverter))]
        public DateTime? DeleteCardTime { get; set; }

        [JsonPropertyName("card_no")]
        public string? CardNo { get; set; }
    }

}