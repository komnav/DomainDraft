using Domain.Entities;

namespace Application.Services;

public interface IRecommendedService
{
    Merchant GetRecommendedMerchant();
    Product GetRecommendedProduct();
    Bundle GetRecommendedBundles();
}