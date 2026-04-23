using Application.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ProductRepository(ApplicationDbContext dbContext) : IProductRepository
{
    public async Task<int> CreateAsync(Product product)
    {
        await dbContext.Products.AddAsync(product);
        return await dbContext.SaveChangesAsync();
    }

    public async Task<int> UpdateAsync(Guid id, string name)
    {
        return await dbContext.Products
            .Where(x => x.Id == id)
            .ExecuteUpdateAsync(s => s
                .SetProperty(x => x.Id, id)
                .SetProperty(x => x.Name, name));
    }

    public async Task<int> DeleteAsync(Guid id)
    {
        return await dbContext.Products.Where(x => x.Id == id).ExecuteDeleteAsync();
    }

    public async Task<List<Product>> GetAllAsync(int page, int pageSize)
    {
        return await dbContext.Products
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }
}