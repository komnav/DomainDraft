using Application.Cache;
using Application.Repositories;
using Application.Services;
using Infrastructure;
using Infrastructure.Cache;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var configuration = builder.Configuration;
builder.Services
    .AddDbContext<ApplicationDbContext>
        (option => option.UseNpgsql(configuration.GetConnectionString("DbConnection")));
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IRecommendedService, RecommendedService>();
builder.Services.AddSingleton<ICacheLayer, CacheLayer>();
builder.Services.AddMemoryCache();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.MapControllers();
app.Run();

namespace DomainDraft.WebApi
{
    public partial class Program
    {
        
    }
}                   