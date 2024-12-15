using System.Text.Json;
using Amazon;
using Amazon.SecretsManager;
using Amazon.SecretsManager.Model;
using YazilimAcademyPayments.WebApi.Settings;

namespace YazilimAcademyPayments.WebApi;

public static class DependencyInjection
{
    /// <summary>
    /// AWS Secrets Manager'dan PayTR ayarlarını çeken ve DI konteynerına kaydeden uzantı metodu.
    /// </summary>
    /// <param name="services">Service collection</param>
    /// <param name="secretName">AWS Secrets Manager Secret Adı. Örneğin: "prod/YazilimAcademyPayments/paytr"</param>
    /// <param name="region">AWS Region örn: "eu-north-1"</param>
    /// <returns></returns>
    public static async Task<IServiceCollection> AddPayTRFromAwsSecretsAsync(this IServiceCollection services, string secretName, string region)
    {
        // AWS Secrets Manager Client oluştur
        IAmazonSecretsManager client = new AmazonSecretsManagerClient(RegionEndpoint.GetBySystemName(region));

        var request = new GetSecretValueRequest
        {
            SecretId = secretName,
            VersionStage = "AWSCURRENT"
        };

        var response = await client.GetSecretValueAsync(request);
        string secretJson = response.SecretString;

        // Secret içeriğini Dictionary olarak deserialize et
        var secretDictionary = JsonSerializer.Deserialize<Dictionary<string, string>>(secretJson);

        // İlgili anahtarlar: "PayTR/MerchantId", "PayTR/MerchantKey", "PayTR/MerchantSalt"
        if (!secretDictionary.TryGetValue("PayTR/MerchantId", out var merchantId))
            throw new InvalidOperationException("PayTR/MerchantId bulunamadı.");

        if (!secretDictionary.TryGetValue("PayTR/MerchantKey", out var merchantKey))
            throw new InvalidOperationException("PayTR/MerchantKey bulunamadı.");

        if (!secretDictionary.TryGetValue("PayTR/MerchantSalt", out var merchantSalt))
            throw new InvalidOperationException("PayTR/MerchantSalt bulunamadı.");

        services.Configure<PayTRSettings>(opts =>
        {
            opts.MerchantId = merchantId;
            opts.MerchantKey = merchantKey;
            opts.MerchantSalt = merchantSalt;
        });

        return services;
    }
}
