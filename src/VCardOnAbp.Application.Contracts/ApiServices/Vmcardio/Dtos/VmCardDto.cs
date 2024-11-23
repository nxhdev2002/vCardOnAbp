using System;
using System.Text.Json.Serialization;
using VCardOnAbp.ApiServices.Vmcardio.Json;

namespace VCardOnAbp.ApiServices.Vmcardio.Dtos;

public class GetCardOutput
{
    public VmCardDto virtual_card { get; set; }
}

public class VmCardDto
{
    public long? id { get; set; }
    public long? uid { get; set; }
    public string? user_name { get; set; }
    public string? card_name { get; set; }
    public string? card_use_name { get; set; }
    public string? tag { get; set; }
    public string? bin { get; set; }
    public string? card_id { get; set; }
    public string? card_number { get; set; }
    public string? card_no { get; set; }
    public string? trans_merchant { get; set; }
    public string? encrypted_cvv { get; set; }
    public string? encrypted_expiration { get; set; }
    public string? last_four { get; set; }
    public int? skip_reconcil { get; set; }
    public string? alias { get; set; }
    public string? status { get; set; }
    [JsonConverter(typeof(NullableDecimalConverter))]
    public decimal? available_amount { get; set; }
    public int? visable { get; set; }
    public string? status_remark { get; set; }
    [JsonConverter(typeof(CustomNullableDateTimeConverter))]
    public DateTime? create_time { get; set; }
    [JsonConverter(typeof(CustomNullableDateTimeConverter))]
    public DateTime? update_time { get; set; }
    [JsonConverter(typeof(CustomNullableDateTimeConverter))]
    public DateTime? frozen_time { get; set; }
    [JsonConverter(typeof(NullableDecimalConverter))]
    public decimal? frozen_amount { get; set; }
    public string? bin_num { get; set; }
    public int? due_days { get; set; }
}
