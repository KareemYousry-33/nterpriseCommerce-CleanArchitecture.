using EnterpriseCommerce.Domain.Entities;

namespace EnterpriseCommerce.Application.Specifications;

public class ProductsWithCategoriesSpecification : BaseSpecification<Product>
{
    // Constructor 1: لجلب قائمة المنتجات مع الفلترة والبحث والترتيب والـ Pagination
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
        // 1. عمل JOIN تلقائي مع جدول الأقسام عشان نجيب اسم القسم
        AddInclude(p => p.Category!);

        // 2. تطبيق الترتيب حسب طلب الفرونت إند
        switch (sort)
        {
            case "priceAsc":
                AddOrderBy(p => p.Price);
                break;
            case "priceDesc":
                AddOrderByDescending(p => p.Price);
                break;
            default:
                AddOrderBy(p => p.Name); // الترتيب الافتراضي أبجدياً بالاسم
                break;
        }

        // 3. تطبيق الـ Pagination (مثلاً صفحة 1 حجمها 10 -> تخطى 0 وهات 10)
        ApplyPaging((pageIndex - 1) * pageSize, pageSize);
    }

    // Constructor 2: لجلب منتج واحد بعينه بالـ ID ومعاه بيانات القسم بتاعه
    public ProductsWithCategoriesSpecification(Guid id)
        : base(p => p.Id == id)
    {
        AddInclude(p => p.Category!);
    }
}