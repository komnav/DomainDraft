using Domain.Domain;

namespace Application.Repositories;

public interface IOrderRepository
{
    Task<int> CreateOrderAsync(Order order);
}