namespace EnterpriseCommerce.Domain.Entities;

using EnterpriseCommerce.Domain.Common;
using EnterpriseCommerce.Domain.Enums;

public class OrderItem : BaseEntity
{
    public Guid ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int Quantity { get; set; }

    public Guid OrderId { get; set; }
    public virtual Order Order { get; set; } = null!;
}

public class Order : BaseEntity
{
    public string BuyerId { get; set; } = string.Empty;
    public OrderStatus Status { get; set; } = OrderStatus.Pending;
    public string ShippingAddress { get; set; } = string.Empty;
    public decimal SubTotal { get; set; }

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}