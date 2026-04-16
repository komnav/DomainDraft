namespace Application.DTOs;

public record OrderDto(
    Guid CustomerId,
    Guid MerchantId,
    Guid BundleId,
    string ProductName,
    string ProductDescription,
    decimal Price
    );