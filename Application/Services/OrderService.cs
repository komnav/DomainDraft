using Application.Cache;
using Application.DTOs;
using Application.Repositories;
using Domain.Entities;

namespace Application.Services;

public class OrderService(
    IOrderRepository orderRepository,
    ICacheLayer cacheLayer) : IOrderService
{
    public async Task<Order> CreateOrderAsync(OrderDto request)
    {
        var newOrder = new Order()
        {
            CustomerId = request.CustomerId,
            BundleId = request.BundleId,
            MerchantId = request.MerchantId,
            Product = new Product()
            {
                Name = request.ProductName,
                Description = request.ProductDescription,
                Price = request.Price
            }
        };
        var addOrder = await orderRepository.CreateOrderAsync(newOrder);
        if (addOrder > 0)
        {
            cacheLayer.RemoveCache("Get_All_Products");
            return new Order()
            {
                CustomerId = request.CustomerId,
                BundleId = request.BundleId,
                MerchantId = request.MerchantId,
                Product = new Product()
                {
                    Name = request.ProductName,
                    Description = request.ProductDescription,
                    Price = request.Price
                }
            };
        }

        return new Order();
    }
}