using Domain.Entities;

namespace Application.Repositories;

public interface IOrderRepository
{
    Task<int> CreateOrderAsync(Order order);
}