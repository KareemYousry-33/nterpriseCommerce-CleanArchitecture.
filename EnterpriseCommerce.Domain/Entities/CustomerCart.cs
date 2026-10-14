namespace EnterpriseCommerce.Domain.Entities;

public class CartItem
{
    public Guid ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public decimal UnitPrice { get; set; }
    public int Quantity { get; set; }
    public string? PictureUrl { get; set; }
}

public class CustomerCart
{
    public string Id { get; set; } = string.Empty; 
    public List<CartItem> Items { get; set; } = new();

    public decimal TotalPrice => Items.Sum(item => item.UnitPrice * item.Quantity);
}