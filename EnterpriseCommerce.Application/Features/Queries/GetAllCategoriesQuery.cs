using EnterpriseCommerce.Application.Common;
using EnterpriseCommerce.Application.Interfaces;
using EnterpriseCommerce.Domain.Entities;
using MediatR;

namespace EnterpriseCommerce.Application.Features.Categories.Queries;

// 1. طلب الاستعلام: مبنحتاجش نبعت معاه أي باراميترز، وبيرجع قائمة من CategoryDto
public record GetAllCategoriesQuery() : IRequest<Result<IReadOnlyList<CategoryDto>>>;

// 2. المعالج (Handler) اللي بيجيب الداتا ويحولها لـ DTO
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