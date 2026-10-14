namespace EnterpriseCommerce.Domain.Entities;
using EnterpriseCommerce.Domain.Common;

    public class Product : BaseEntity

    {
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string? ImageUrl { get; set; }
    public int StockQuantity { get; set; }
    public Guid CategoryId { get; set; }
    public virtual Category Category { get; set; } = null!;
}

