using YazilimAcademyPayments.WebApi.Domain.Common;

namespace YazilimAcademyPayments.WebApi.Domain.Entities;

public sealed class Tenant : EntityBase
{
    public string Name { get; set; } = null!;
    public string Domain { get; set; } = null!;
    public string BaseUrl { get; set; } = null!;
    public string SuccessPath { get; set; } = null!;
    public string FailPath { get; set; } = null!;

    public ICollection<User> Users { get; set; } = [];
    public ICollection<Order> Orders { get; set; } = [];
}
