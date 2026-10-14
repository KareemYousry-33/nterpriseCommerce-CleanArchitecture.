using EnterpriseCommerce.Application.Specifications;
using EnterpriseCommerce.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace EnterpriseCommerce.Infrastructure.Data;

public static class SpecificationEvaluator<TEntity> where TEntity : BaseEntity
{
    public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery, ISpecification<TEntity> spec)
    {
        var query = inputQuery;

        // 1. تطبيق شرط الفلترة (Where) لو موجود
        if (spec.Criteria is not null)
        {
            query = query.Where(spec.Criteria);
        }

        // 2. تطبيق الترتيب التصاعدي أو التنازلي (OrderBy / OrderByDescending)
        if (spec.OrderBy is not null)
        {
            query = query.OrderBy(spec.OrderBy);
        }
        else if (spec.OrderByDescending is not null)
        {
            query = query.OrderByDescending(spec.OrderByDescending);
        }

        // 3. تطبيق تقسيم الصفحات (Pagination: Skip & Take)
        // مهم جداً: الـ Paging لازم يتطبق بعد الفلترة والترتيب!
        if (spec.IsPagingEnabled)
        {
            query = query.Skip(spec.Skip).Take(spec.Take);
        }

        // 4. تجميع وإضافة كل الجداول المرتبطة (Includes) باستخدام Aggregate
        query = spec.Includes.Aggregate(query, (current, include) => current.Include(include));

        return query;
    }
}