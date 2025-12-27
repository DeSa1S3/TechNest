using Backend.Tables;
using Microsoft.EntityFrameworkCore;
using System;

namespace TechNest.Backend.Data;

public class DataContext : DbContext
{
    public DataContext() { } // Пустой конструктор

    public DataContext(DbContextOptions<DataContext> options) : base(options) { }

    public DbSet<UsersTable> user_table { get; set; }
    public DbSet<ShopItemsTable> shop_items { get; set; }
    public DbSet<CatalogTable> catalog_tables { get; set; }
    public DbSet<NewsTable> news_tables { get; set; }
    public DbSet<RuleTable> rule_tables { get; set; }
    public DbSet<MaybeLikeTable> maybe_like { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseNpgsql("Server=localhost;Database=TechNestDB;Port=5432;User Id=user;Password=root;");
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

 
    }
}