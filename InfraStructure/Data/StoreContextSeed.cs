using System;
using System.Text.Json;
using Core.Entities;

namespace InfraStructure.Data;

public class StoreContextSeed
{
    public static async Task SeedAsync(StoreContext context)
    {
        if (!context.Products.Any())
        {
            var productsData = await File.ReadAllTextAsync("../InfraStructure/Data/SeedData/products.json");
            var products = JsonSerializer.Deserialize<List<Product>>(productsData);
            if (products == null) return;
            context.Products.AddRange(products);
            await context.SaveChangesAsync();
        }
    }
}
