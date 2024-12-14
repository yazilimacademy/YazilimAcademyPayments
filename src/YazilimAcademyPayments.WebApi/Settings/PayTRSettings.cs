namespace YazilimAcademyPayments.WebApi.Settings;

public sealed record PayTRSettings
{
    public string MerchantId { get; set; }
    public string MerchantKey { get; set; }
    public string MerchantSalt { get; set; }
}
