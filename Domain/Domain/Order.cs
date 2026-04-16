namespace Domain.Domain;

public class Order
{
    public Guid Id { get; set; }
    public Guid CustomerId { get; set; }
    
    public Customer Customer { get; set; }
    
    public Guid MerchantId { get; set; }
    public Merchant Merchant { get; set; }
    
    public Guid BundleId { get; set; }
    public Bundle Bundle { get; set; }
    public Product Product { get; set; }
}