using Microsoft.EntityFrameworkCore;
using YazilimAcademyPayments.WebApi.Domain.Entities;

namespace YazilimAcademyPayments.WebApi.Persistence.EntityFramework.Contexts;

public sealed class ApplicationDbContext : DbContext
{
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderHistory> OrderHistories { get; set; }
    public DbSet<Promotion> Promotions { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Tenant> Tenants { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Payment> Payments { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
}
