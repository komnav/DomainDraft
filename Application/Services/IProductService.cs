using Application.DTOs;
using Domain.Entities;

namespace Application.Services;

public interface IProductService
{
    Task<int>  CreateAsync(ProductDto request);
    Task<int>  UpdateAsync(Guid id, string name);
    Task<int>  DeleteAsync(Guid id);
    Task<List<Product>>  GetAllAsync();
}