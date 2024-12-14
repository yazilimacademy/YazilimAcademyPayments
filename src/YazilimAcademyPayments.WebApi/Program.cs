using Microsoft.AspNetCore.Authentication;
using YazilimAcademyPayments.WebApi.Settings;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = "ApiKeyScheme";
    options.DefaultChallengeScheme = "ApiKeyScheme";
})
.AddScheme<AuthenticationSchemeOptions, ApiKeyAuthenticationHandler>("ApiKeyScheme", null);

builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = "localhost:6379"; // Redis sunucu adresiniz
    options.InstanceName = "YazilimAcademyPayments_";
});

builder.Services.Configure<PayTRSettings>(builder.Configuration.GetSection(nameof(PayTRSettings)));



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
