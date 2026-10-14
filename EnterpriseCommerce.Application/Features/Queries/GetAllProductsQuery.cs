using EnterpriseCommerce.Application.Common;
using EnterpriseCommerce.Application.Interfaces;
using EnterpriseCommerce.Application.Specifications;
using EnterpriseCommerce.Domain.Entities;
using MediatR;

namespace EnterpriseCommerce.Application.Features.Products.Queries;

// لاحظ القيم الافتراضية للـ Pagination: صفحة 1، وحجم الصفحة 10 منتجات
public record GetAllProductsQuery(
    Guid? CategoryId = null,
    string? Search = null,
    string? Sort = null,
    int PageIndex = 1,
    int PageSize = 10) : IRequest<Result<IReadOnlyList<ProductDto>>>;

public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, Result<IReadOnlyList<ProductDto>>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAllProductsQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<IReadOnlyList<ProductDto>>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
    {
        // نجهز الورقة (الـ Specification) بكل الشروط اللي بعتها الفرونت إند
        var spec = new ProductsWithCategoriesSpecification(
            request.CategoryId,
            request.Search,
            request.Sort,
            request.PageIndex,
            request.PageSize);

        // نبعتها للـ Repository ينفذها ويرجعلنا المنتجات شاملة القسم!
        var products = await _unitOfWork.Repository<Product>().ListWithSpecAsync(spec, cancellationToken);

        var dtos = products.Select(p => new ProductDto(
            p.Id,
            p.Name,
            p.Description,
            p.Price,
            p.StockQuantity,
            p.ImageUrl,
            p.CategoryId,
            p.Category?.Name ?? "Uncategorized"
        )).ToList();

        return Result<IReadOnlyList<ProductDto>>.Success(dtos);
    }
}