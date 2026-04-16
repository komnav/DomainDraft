using Domain.Domain;

namespace Application.Repositories;

public interface IProductRepository
{
    Task<int>  CreateAsync(Product product);
    Task<int>  UpdateAsync(Guid id, string name);
    Task<int>  DeleteAsync(Guid id);
    Task<List<Product>>  GetAllAsync();
}