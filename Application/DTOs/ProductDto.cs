namespace Application.DTOs;

public record ProductDto(
    string ProductName,
    string ProductDescription,
    decimal Price);