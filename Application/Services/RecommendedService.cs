using Application.Cache;
using Domain.Domain;

namespace Application.Services;

public class RecommendedService(
    ICacheLayer cacheLayer)
    : IRecommendedService
{
    private const string MockMerchant = "Amid";
    private const string MockProduct = "Apple";
    private const string MockBundles = "KFC";

    public Merchant GetRecommendedMerchant()
    {
        const string key = "Get_Recommended_Merchant";
        var cachedData = cacheLayer.GetCache<Merchant>(key);
        if (cachedData != null)
        {
            return cachedData;
        }

        var newMerchant = new Merchant
        {
            Id = new Guid(),
            Name = MockMerchant
        };
        cacheLayer.SetToCache(key, newMerchant, TimeSpan.FromHours(12));
        return newMerchant;
    }

    public Product GetRecommendedProduct()
    {
        const string key = "Get_Recommended_Product";
        var cachedData = cacheLayer.GetCache<Product>(key);
        if (cachedData != null)
        {
            return cachedData;
        }

        var newProduct = new Product
        {
            Id = new Guid(),
            Name = MockProduct,
            Description = "Description",
            Price = 12
        };
        cacheLayer.SetToCache(key, newProduct, TimeSpan.FromHours(12));
        return newProduct;
    }

    public Bundle GetRecommendedBundles()
    {
        const string key = "Get_Recommended_Bundles";
        var cachedData = cacheLayer.GetCache<Bundle>(key);
        if (cachedData != null)
        {
            return cachedData;
        }

        var newBundles = new Bundle
        {
            Id = new Guid(),
            Name = MockBundles
        };
        cacheLayer.SetToCache(key, newBundles, TimeSpan.FromHours(12));
        return newBundles;
    }
}