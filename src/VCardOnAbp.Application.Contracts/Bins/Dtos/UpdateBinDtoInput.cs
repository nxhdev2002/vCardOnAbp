namespace VCardOnAbp.Bins.Dtos
{
    public class UpdateBinDtoInput
    {
        public decimal? CreationFixedFee { get; set; }
        public decimal? CreationPercentFee { get; set; }
        public decimal? FundingFixedFee { get; set; }
        public decimal? FundingPercentFee { get; set; }
    }
}
