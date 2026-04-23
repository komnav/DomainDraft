// using Application.Cache;
// using Infrastructure.Cache;
// using Microsoft.Extensions.Configuration;
// using Microsoft.Extensions.DependencyInjection;
//
// namespace Infrastructure;
//
// public static class InfrastructureServiceRegistration
// {
//     public static IServiceCollection AddInfrastructureService(
//         this IServiceCollection service,
//         IConfiguration configuration)
//     {
//         service.AddStackExchangeRedisCache(options =>
//         {
//             options.Configuration = configuration["Redis:Configuration"];
//             options.InstanceName = configuration["Redis:InstanceName"];
//         // });
//
//         service.AddScoped<ICacheService, CacheService>();
//         return service;
//     }
// }