using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;

namespace TechNest.Backend.Data; // или ваше пространство имён

public class DataContext : DbContext    
{
    public DataContext() => Database.EnsureCreated();

    public DataContext(DbContextOptions<DataContext> options) : base(options) { }

    // public DbSet<UserTable> user_table { get; set; } 
  
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseNpgsql("Server=texhnest_db;Database=TechNestDB;Port=5432;User Id=DeSa1S13-user;Password=root;");
        }
    }
}