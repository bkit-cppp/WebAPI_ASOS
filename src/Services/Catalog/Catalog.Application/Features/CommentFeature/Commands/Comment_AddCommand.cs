using BuildingBlock.Grpc.Services;
using Catalog.Application.Features.CommentFeature.Dto;
using FluentValidation;

namespace Catalog.Application.Features.CommentFeature.Commands;
public record Comment_AddCommand(Comment RequestData) : ICommand<Result<CommentDto>>;

public class CommentAddCommandValidator : AbstractValidator<Comment_AddCommand>
{
	public CommentAddCommandValidator()
	{
		RuleFor(command => command.RequestData.UserId)
			.NotEmpty().WithMessage("UserId is required");

		RuleFor(command => command.RequestData.Content)
			.NotEmpty().WithMessage("Content is required");

		RuleFor(command => command.RequestData.ProductId)
			.NotEmpty().WithMessage("Product is required");
	}
}
public class Comment_AddCommandHandler : ICommandHandler<Comment_AddCommand, Result<CommentDto>>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IMapper _mapper;
	private readonly IdentityGrpcService _identityService;
	public Comment_AddCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, IdentityGrpcService identityService)
	{
		_unitOfWork = unitOfWork;
		_mapper = mapper;
		_identityService = identityService;
	}
	public async Task<Result<CommentDto>> Handle(Comment_AddCommand request, CancellationToken cancellationToken)
	{
		var product = await _unitOfWork.Products.Queryable()
									   .Where(s => s.Id == request.RequestData.ProductId).FirstOrDefaultAsync();

		if (product == null)
		{
			throw new ApplicationException("Product not found");
		}

		var user = await _unitOfWork.UserComments.FindAsync(request.RequestData.UserId!.Value);
		if (user == null)
		{
			var data = await _identityService.GetUserAsync(request.RequestData.UserId!.Value);
			user = new UserComment()
			{
				Id = request.RequestData.UserId!.Value,
				Avatar = data.Avatar,
				Email = data.Email,
				Fullname = data.Fullname,
			};
			_unitOfWork.UserComments.Add(user, request.RequestData.UserId!.Value);
		}

		var comment = new Comment()
		{
			User = user,
			UserId = user.Id,
			Content = request.RequestData.Content,
			ProductId = request.RequestData.ProductId
		};

		if (request.RequestData.ParentId != null)
		{
			var parent = await _unitOfWork.Comments.Queryable()
						.Where(s => s.Id == request.RequestData.ParentId.Value)
						.FirstOrDefaultAsync();

			if (parent == null)
			{
				throw new ApplicationException("Parent comment not found");
			}

			comment.ParentId = parent.Id;
		}

		_unitOfWork.Comments.Add(comment, request.RequestData.CreatedUser);
		int rows = await _unitOfWork.CompleteAsync();
		if (rows > 0)
		{
			await _unitOfWork.RemoveCacheAsync($"{CatalogCacheKey.Comment}/product/{product.Id}");
		}

		return Result<CommentDto>.Success(_mapper.Map<CommentDto>(comment));

	}
}
