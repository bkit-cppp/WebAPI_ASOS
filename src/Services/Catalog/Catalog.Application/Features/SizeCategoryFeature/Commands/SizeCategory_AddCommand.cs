using Catalog.Application.Features.SizeCategoryFeature.Dto;
using FluentValidation;
using MediatR;

namespace Catalog.Application.Features.SizeCategoryFeature.Commands;

public record SizeCategory_AddCommand(SizeCategory RequestData) : ICommand<Result<SizeCategoryDto>>;

public class SizeCategoryAddCommandValidator : AbstractValidator<SizeCategory_AddCommand>
{
    public SizeCategoryAddCommandValidator()
    {
        RuleFor(command => command.RequestData.Id)
            .NotEmpty().WithMessage("Id is required");

        RuleFor(command => command.RequestData.SizeId)
            .NotEmpty().WithMessage("SizeId is required");

        RuleFor(command => command.RequestData.CategoryId)
            .NotEmpty().WithMessage("CategoryId is required");
    }
}

public class SizeCategory_AddCommandHandler : ICommandHandler<SizeCategory_AddCommand, Result<SizeCategoryDto>>
{

    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public SizeCategory_AddCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<SizeCategory>> Handle(SizeCategory_AddCommand request, Gender SizeCategories, CancellationToken cancellationToken)
    {
        var SizeCategory = new SizeCategory()
        {
            Id = request.RequestData.Id,
            SizeId = request.RequestData.SizeId,
            CategoryId = request.RequestData.CategoryId
        };

        _unitOfWork.Genders.Add(SizeCategories, request.RequestData.CreatedUser);
        await _unitOfWork.CompleteAsync();

        return Result<SizeCategory>.Success(SizeCategory);
    }

    Task<Result<SizeCategoryDto>> IRequestHandler<SizeCategory_AddCommand, Result<SizeCategoryDto>>.Handle(SizeCategory_AddCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}