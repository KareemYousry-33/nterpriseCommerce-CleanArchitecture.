using EnterpriseCommerce.Application.Common;
using EnterpriseCommerce.Application.Interfaces;
using EnterpriseCommerce.Domain.Entities;
using MediatR;

namespace EnterpriseCommerce.Application.Features.Categories.Queries;

public record GetAllCategoriesQuery() : IRequest<Result<IReadOnlyList<CategoryDto>>>;

public class GetAllCategoriesQueryHandler : IRequestHandler<GetAllCategoriesQuery, Result<IReadOnlyList<CategoryDto>>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAllCategoriesQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<IReadOnlyList<CategoryDto>>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
    {
        var categories = await _unitOfWork.Repository<Category>().GetAllAsync(cancellationToken);

        var dtos = categories
            .Select(c => new CategoryDto(c.Id, c.Name, c.Description))
            .ToList();

        return Result<IReadOnlyList<CategoryDto>>.Success(dtos);
    }
}