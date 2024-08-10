using System;
using Core.Entities;
using Core.Interfaces;
using InfraStructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;
[ApiController]
[Route("api/[controller]")]
public class ProductController(IProductRepo repo) : ControllerBase
{
    
    [HttpGet]   
    public async Task<ActionResult<IReadOnlyList<Product>>> GetProducts(string? brand, string? type, string? sort)
    {
        if (!string.IsNullOrWhiteSpace(brand) || !string.IsNullOrWhiteSpace(type) || !string.IsNullOrWhiteSpace(sort))
            return Ok(await repo.GetProductsAsync(brand, type, sort));
        return Ok(await repo.GetProductsAsync(null, null, null));
    }
    
    [HttpGet("{id:int}")]
    public async Task<ActionResult<Product>> GetProduct(int id)
    {
        var product = await repo.GetProductByIdAsync(id);
        if (product == null) return NotFound();
        return product;
    }
    [HttpPost]
    public async Task<ActionResult<Product>> CreateProduct(Product product)
    {
        repo.AddProduct(product);
        if (!await repo.SaveChangesAsync()) return BadRequest("Failed to save product");
        return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
    }
    [HttpPut("{id:int}")]
    public async Task<ActionResult<Product>> UpdateProduct(int id, Product product)
    {
        if (id != product.Id || !await ProductExists(id)) return BadRequest("Product ID mismatch");
        repo.UpdateProduct(product);
        if (!await repo.SaveChangesAsync()) return BadRequest("Failed to update product");
        return NoContent();
    }

    private async Task<bool> ProductExists(int id)
    {
        return await repo.ProductExists(id);
    }
    [HttpGet("brands")]
    public async Task<ActionResult<IReadOnlyList<string>>> GetBrands()
    {
        return Ok(await repo.GetBrandsAsync());
    }

    [HttpGet("types")]
    public async Task<ActionResult<IReadOnlyList<string>>> GetTypes()
    {
        return Ok(await repo.GetTypesAsync());
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteProduct(int id)
    {
        var product = await repo.GetProductByIdAsync(id);
        if (product == null) return NotFound();
        repo.DeleteProduct(product);
        if (!await repo.SaveChangesAsync()) return BadRequest("Failed to delete product");
        return NoContent();
    }
}
