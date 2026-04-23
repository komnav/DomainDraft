using Application.Cache;
using Application.DTOs;
using Application.Repositories;
using Domain.Entities;

namespace Application.Services;

public class ProductService(
    IProductRepository productRepository,
    ICacheLayer cacheLayer) : IProductService

{
    private const int PageSize = 4;
    private const string CacheKey = "Get_All_Products_by_page_";

    public async Task<int> CreateAsync(ProductDto request)
    {
        var newProduct = new Product()
        {
            Name = request.ProductName,
            Description = request.ProductDescription,
            Price = request.Price
        };
        var addProduct = await productRepository.CreateAsync(newProduct);
        if (addProduct > 0)
        {
            cacheLayer.ResetCache();
            return 1;
        }

        return 0;
    }

    public async Task<int> UpdateAsync(Guid id, string name)
    {
        var update = await productRepository.UpdateAsync(id, name);
        if (update > 0)
        {
            cacheLayer.ResetCache();
            return 1;
        }

        return 0;
    }

    public async Task<int> DeleteAsync(Guid id)
    {
        var delete = await productRepository.DeleteAsync(id);
        if (delete > 0)
        {
            cacheLayer.ResetCache();
            return 1;
        }

        return 0;
    }

    public async Task<List<Product>> GetAllAsync(int? requestPage)
    {
        var page = requestPage ?? 1;

        var cached = cacheLayer.GetCache<List<Product>>($"{CacheKey}{page}");
        if (cached != null)
        {
            return cached;
        }

        var listOfProducts = await productRepository.GetAllAsync(page, PageSize);

        cacheLayer.SetToCache($"{CacheKey}{page}", listOfProducts, TimeSpan.FromMinutes(10));

        return listOfProducts;
    }
}