using Application.DTOs;
using Application.Services;
using Domain.Domain;
using Microsoft.AspNetCore.Mvc;

namespace DomainDraft.WebApi.Controllers;

[ApiController]
[Route("Product")]
public class ProductControllers(
    IProductService productService) : ControllerBase
{
    [HttpPost]
    public async Task<int> AddAsync(ProductDto request)
    {
        return await productService.CreateAsync(request);
    }

    [HttpPut]
    public async Task<int> UpdateAsync(Guid id, string name)
    {
        return await productService.UpdateAsync(id, name);
    }

    [HttpDelete]
    public async Task<int> Delete(Guid id)
    {
        return await productService.DeleteAsync(id);
    }

    [HttpGet("all")]
    public async Task<List<Product>> GetAllAsync()
    {
        return await productService.GetAllAsync();
    }
}