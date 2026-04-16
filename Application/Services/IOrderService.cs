using Application.DTOs;
using Domain.Domain;

namespace Application.Services;

public interface IOrderService
{
    Task<Order> CreateOrderAsync(OrderDto request);
}