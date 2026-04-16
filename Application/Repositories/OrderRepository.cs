using Domain.Domain;
using Infrastructure;

namespace Application.Repositories;

public class OrderRepository(ApplicationDbContext applicationDbContext) : IOrderRepository
{
    public async Task<int> CreateOrderAsync(Order order)
    {
        await applicationDbContext.Orders.AddAsync(order);
        return await applicationDbContext.SaveChangesAsync();
    }
}