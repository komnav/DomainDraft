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
    const string IsValidKey = "Product_is_valid";

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
            InvalidateKey(IsValidKey);
            return 1;
        }

        return 0;
    }

    public async Task<int> UpdateAsync(Guid id, string name)
    {
        var update = await productRepository.UpdateAsync(id, name);
        if (update > 0)
        {
            InvalidateKey(IsValidKey);
            return 1;
        }

        return 0;
    }

    public async Task<int> DeleteAsync(Guid id)
    {
        var delete = await productRepository.DeleteAsync(id);
        if (delete > 0)
        {
            InvalidateKey(IsValidKey);
            return 1;
        }

        return 0;
    }

    public async Task<List<Product>> GetAllAsync(int? requestPage)
    {
        var page = requestPage ?? 1;
        var isValid = cacheLayer.TryGet<bool>(IsValidKey);

        if (isValid is { KeyExist: true })
        {
            var cached = cacheLayer.TryGet<List<Product>>($"{CacheKey}{page}");
            if (cached is { KeyExist: true, Value: not null })
            {
                return cached.Value;
            }
        }

        var listOfProducts = await productRepository.GetAllAsync(page, PageSize);

        cacheLayer.SetToCache($"{CacheKey}{page}", listOfProducts, TimeSpan.FromMinutes(10));
        cacheLayer.SetToCache(IsValidKey, true, TimeSpan.FromMinutes(10));

        return listOfProducts;
    }

    private void InvalidateKey(string key)
    {
        cacheLayer.RemoveCache(key);
    }
}