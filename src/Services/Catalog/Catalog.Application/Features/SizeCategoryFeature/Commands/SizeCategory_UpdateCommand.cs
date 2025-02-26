
using Catalog.Application.Features.SizeCategoryFeature.Dto;
using FluentValidation;

namespace Catalog.Application.Features.SizeCategoryFeature.Commands;

public record SizeCategory_UpdateCommand(SizeCategory RequestData) : ICommand<Result<SizeCategoryDto>>;

public class SizeCategoryUpdateCommandValidator : AbstractValidator<SizeCategory_UpdateCommand>
{
    public SizeCategoryUpdateCommandValidator()
    {
        RuleFor(command => command.RequestData.Id)
            .NotEmpty().WithMessage("Id is required");

        RuleFor(command => command.RequestData.SizeId)
            .NotEmpty().WithMessage("SizeId is required");

        RuleFor(command => command.RequestData.CategoryId)
            .NotEmpty().WithMessage("CategoryId is required");
    }
}

public class SizeCategory_UpdateCommandHandler : ICommandHandler<SizeCategory_UpdateCommand, Result<SizeCategoryDto>>
{

    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public SizeCategory_UpdateCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<SizeCategoryDto>> Handle(SizeCategory_UpdateCommand request, CancellationToken cancellationToken)
    {

        var SizeCategory = await _unitOfWork.SizeCategories.FindAsync(request.RequestData.Id, true);

        SizeCategory!.Category = request.RequestData.Category;
        SizeCategory.ModifiedDate = DateTime.Now;
        SizeCategory.ModifiedUser = request.RequestData.ModifiedUser;
        _unitOfWork.SizeCategories.Update(SizeCategory, request.RequestData.ModifiedUser);
        await _unitOfWork.CompleteAsync();

        return Result<SizeCategoryDto>.Success(_mapper.Map<SizeCategoryDto>(SizeCategory));
    }
}