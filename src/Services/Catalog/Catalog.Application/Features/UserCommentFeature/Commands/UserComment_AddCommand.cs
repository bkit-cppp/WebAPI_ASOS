using Catalog.Application.Features.UserCommentFeature.Dto;
using FluentValidation;

namespace Catalog.Application.Features.UserCommentFeature.Commands
{
    public record UserComment_AddCommand(UserCommentDto RequestData) : ICommand<Result<UserCommentDto>>;

    public class UserCommentAddCommandValidator : AbstractValidator<UserComment_AddCommand>
    {
        public UserCommentAddCommandValidator()
        {
            RuleFor(command => command.RequestData.Email)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("A valid email address is required");

            RuleFor(command => command.RequestData.Fullname)
                .NotEmpty().WithMessage("Full name is required");
        }
    }

    public class UserComment_AddCommandHandler : ICommandHandler<UserComment_AddCommand, Result<UserCommentDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UserComment_AddCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<UserCommentDto>> Handle(UserComment_AddCommand request, CancellationToken cancellationToken)
        {
            var userComment = new UserComment
            {
                Email = request.RequestData.Email,
                Fullname = request.RequestData.Fullname,
            };

            _unitOfWork.UserComments.Add(userComment); 
            await _unitOfWork.CompleteAsync();

            return Result<UserCommentDto>.Success(_mapper.Map<UserCommentDto>(userComment));
        }
    }
}
