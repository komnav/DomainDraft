using Application.DTOs;
using Domain.Entities;

namespace Application.Services;

public interface IOrderService
{
    Task<Order> CreateOrderAsync(OrderDto request);
}