using System.Linq.Expressions;
using EnterpriseCommerce.Domain.Common;

namespace EnterpriseCommerce.Application.Specifications;

public interface ISpecification<T> where T : BaseEntity
{
    // شرط الفلترة: Where(x => ...)
    Expression<Func<T, bool>>? Criteria { get; }

    // الجداول المرتبطة اللي عايزين نجيبها معانا: Include(x => x.Category)
    List<Expression<Func<T, object>>> Includes { get; }

    // الترتيب التصاعدي والتنازلي
    Expression<Func<T, object>>? OrderBy { get; }
    Expression<Func<T, object>>? OrderByDescending { get; }

    // خصائص الـ Pagination (تقسيم الصفحات)
    int Take { get; }
    int Skip { get; }
    bool IsPagingEnabled { get; }
}