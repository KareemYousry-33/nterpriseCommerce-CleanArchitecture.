using EnterpriseCommerce.Application.Common;
using EnterpriseCommerce.Application.Interfaces;
using EnterpriseCommerce.Domain.Entities;
using FluentValidation;
using MediatR;

namespace EnterpriseCommerce.Application.Features.Categories.Commands;

// 1. الطلب نفسه (البيانات اللي جاية من اليوزر) وبيرجع Result فيها الـ Id الجديد
public record CreateCategoryCommand(string Name, string Description) : IRequest<Result<Guid>>;

// 2. التحقق من البيانات (Validation) قبل ما الكود يشتغل أصلاً
public class CreateCategoryValidator : AbstractValidator<CreateCategoryCommand>
{
    public CreateCategoryValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("اسم القسم مطلوب")
            .MaximumLength(100).WithMessage("اسم القسم لا يجب أن يتجاوز 100 حرف");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("وصف القسم مطلوب");
    }
}

// 3. المنفذ الفعلي للعملية (Handler)
public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, Result<Guid>>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateCategoryCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = new Category
        {
            Name = request.Name,
            Description = request.Description
        };

        await _unitOfWork.Repository<Category>().AddAsync(category, cancellationToken);
        await _unitOfWork.CompleteAsync(cancellationToken);

        return Result<Guid>.Success(category.Id);
    }
}