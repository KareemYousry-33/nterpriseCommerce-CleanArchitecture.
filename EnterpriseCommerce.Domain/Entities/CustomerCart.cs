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
    public string Id { get; set; } = string.Empty; // هيكون هو الـ UserId
    public List<CartItem> Items { get; set; } = new();

    // خاصية بتحسب الإجمالي أوتوماتيك من غير ما نخزنها
    public decimal TotalPrice => Items.Sum(item => item.UnitPrice * item.Quantity);
}