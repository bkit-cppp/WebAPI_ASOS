using Promotion.API.Data;
namespace Promotion.API.Features.DiscountHistoryFeature.Commands
{
    public record DiscountHistory_DeleteCommand(DeleteRequest RequestData) : ICommand<Result<bool>>;
    public class DiscountHistory_DeleteCommandHandler : ICommandHandler<DiscountHistory_DeleteCommand, Result<bool>>
    {

        private readonly DataContext _context;

        public DiscountHistory_DeleteCommandHandler(IMapper mapper, DataContext context)
        {
            _context = context;
        }
        public async Task<Result<bool>> Handle(DiscountHistory_DeleteCommand request, CancellationToken cancellationToken)
        {
            if (request.RequestData.Ids == null)
                throw new ApplicationException("Ids not found");

            List<Guid> ids = request.RequestData.Ids.Select(m => Guid.Parse(m)).ToList();
            var query = await _context.DiscountHistories.Where(m => ids.Contains(m.Id)).ToListAsync();
            if (query == null || query.Count == 0) throw new ApplicationException($"Không tìm thấy trong dữ liệu có Id: {string.Join(";", request.RequestData.Ids)}");

			foreach (var item in query)
			{
				item.DeleteFlag = true;
				item.ModifiedDate = DateTime.Now;
				item.ModifiedUser = request.RequestData.ModifiedUser;
			}

			_context.DiscountHistories.UpdateRange(query);

			await _context.SaveChangesAsync(cancellationToken);
            return Result<bool>.Success(true);
        }
    }
}
