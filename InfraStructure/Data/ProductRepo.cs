using System;
using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InfraStructure.Data;

public class ProductRepo(StoreContext context) : IProductRepo
{
    public void AddProduct(Product product)
    {
        context.Products.Add(product);
    }

    public void DeleteProduct(Product product)
    {
        context.Products.Remove(product);
    }

    public async Task<IReadOnlyList<string>> GetBrandsAsync()
    {
        return await context.Products.Select(x => x.Brand).Distinct().ToListAsync();
    }

    public async Task<Product?> GetProductByIdAsync(int id)
    {
        return await context.Products.FindAsync(id);
    }

    public async Task<IReadOnlyList<Product>> GetProductsAsync(string? brand, string? type, string? sort)
    {
        var products = context.Products.AsQueryable();
        
        if (!string.IsNullOrWhiteSpace(brand)) products = products.Where(x => x.Brand == brand);
       
        if (!string.IsNullOrWhiteSpace(type)) products = products.Where(x => x.Type == type);
        
        products = sort switch
        {
            "priceAsc" => products.OrderBy(x => x.Price),
            "priceDesc" => products.OrderByDescending(x => x.Price),
            _ => products.OrderBy(x => x.Name)
        };
        
        return await products.Skip(5).Take(5).ToListAsync();
    }
    

    public async Task<IReadOnlyList<string>> GetTypesAsync()
    {
        return await context.Products.Select(x => x.Type).Distinct().ToListAsync();
    }

    public async Task<bool> ProductExists(int id)
    {
        return await context.Products.AnyAsync(x => x.Id == id);
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await context.SaveChangesAsync() > 0;
    }

    public void UpdateProduct(Product product)
    {
        context.Entry(product).State = EntityState.Modified;
    }
}
