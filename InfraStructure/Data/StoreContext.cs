using System;
using Core.Entities;
using InfraStructure.Config;
using Microsoft.EntityFrameworkCore;

namespace InfraStructure.Data;

public class StoreContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Product> Products { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProductConfig).Assembly);
    }
}
