using Domain.Domain;

namespace Application.Services;

public interface IRecommendedService
{
    Merchant GetRecommendedMerchant();
    Product GetRecommendedProduct();
    Bundle GetRecommendedBundles();
}