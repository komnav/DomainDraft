using Application.DTOs;
using Application.Services;
using Domain.Domain;
using Microsoft.AspNetCore.Mvc;

namespace DomainDraft.WebApi.Controllers;

[ApiController]
[Route("Controller")]
public class OrderController(IOrderService orderService) : ControllerBase
{
    [HttpPost]
    public Task<Order> CreateOrder(OrderDto request)
    {
        return orderService.CreateOrderAsync(request);
    }
}