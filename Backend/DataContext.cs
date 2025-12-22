using Backend.Tables;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;

namespace TechNest.Backend.Data; 

public class DataContext : DbContext    
{
    public DataContext() => Database.EnsureCreated();

    public DataContext(DbContextOptions<DataContext> options) : base(options) { }

    public DbSet<UsersTable> user_table { get; set; } 

    public DbSet<ShopItemsTable> shop_items { get; set; }
  
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseNpgsql("Server=postgres_db;Database=TechNestDB;Port=5432;User Id=user;Password=root;");
        }
    }
}