using InventoryManagement.Api.Data.Entity;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.Api.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

    public DbSet<Product> Products => Set<Product>();
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<InventoryTransaction> Transactions => Set<InventoryTransaction>();
    public DbSet<Supplier> Suppliers => Set<Supplier>();
}
