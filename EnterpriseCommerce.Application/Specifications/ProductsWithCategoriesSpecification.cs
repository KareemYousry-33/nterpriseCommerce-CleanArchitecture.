using EnterpriseCommerce.Domain.Entities;

namespace EnterpriseCommerce.Application.Specifications;

public class ProductsWithCategoriesSpecification : BaseSpecification<Product>
{
    public ProductsWithCategoriesSpecification(
        Guid? categoryId,
        string? search,
        string? sort,
        int pageIndex,
        int pageSize)
        : base(p =>
            (string.IsNullOrEmpty(search) || p.Name.ToLower().Contains(search.ToLower())) &&
            (!categoryId.HasValue || p.CategoryId == categoryId.Value)
        )
    {
        AddInclude(p => p.Category!);

        switch (sort)
        {
            case "priceAsc":
                AddOrderBy(p => p.Price);
                break;
            case "priceDesc":
                AddOrderByDescending(p => p.Price);
                break;
            default:
                AddOrderBy(p => p.Name); 
                break;
        }

        ApplyPaging((pageIndex - 1) * pageSize, pageSize);
    }

    public ProductsWithCategoriesSpecification(Guid id)
        : base(p => p.Id == id)
    {
        AddInclude(p => p.Category!);
    }
}