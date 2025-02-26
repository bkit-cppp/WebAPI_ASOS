using Catalog.Application.Features.CommentFeature.Dto;
using Catalog.Application.Features.GenderFeature.Commands;
using Catalog.Application.Features.GenderFeature.Dto;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Application.Features.CommentFeature.Commands;

public record Comment_UpdateCommand(Comment RequestData) : ICommand<Result<CommentDto>>;

public class CommentUpdateCommandValidator : AbstractValidator<Comment_UpdateCommand>
{ 
     public CommentUpdateCommandValidator()
     {
        RuleFor(command => command.RequestData.Id).NotEmpty().WithMessage("Id is required");
        
        RuleFor(command => command.RequestData.Content).NotEmpty().WithMessage("Contentis required");

     }
}
public class Gender_UpdateCommandHandler : ICommandHandler<Comment_UpdateCommand, Result<CommentDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public Gender_UpdateCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<CommentDto>> Handle(Comment_UpdateCommand request, CancellationToken cancellationToken)
    {        
        var comment = await _unitOfWork.Comments.FindAsync(request.RequestData.Id, true);
        
        comment!.Content = request.RequestData.Content;
        comment.ModifiedDate = DateTime.Now;
        comment.ModifiedUser = request.RequestData.ModifiedUser;
        _unitOfWork.Comments.Update(comment!, request.RequestData.ModifiedUser);
        int rows = await _unitOfWork.CompleteAsync();
		if (rows > 0)
		{
			await _unitOfWork.RemoveCacheAsync(CatalogCacheKey.Comment);
		}

		return Result<CommentDto>.Success(_mapper.Map<CommentDto>(comment));
    }

}
