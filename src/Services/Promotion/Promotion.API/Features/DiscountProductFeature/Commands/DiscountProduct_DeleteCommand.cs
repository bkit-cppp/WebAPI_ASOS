using Promotion.API.Data;
namespace Promotion.API.Features.DiscountProductFeature.Commands
{
    public record DiscountProduct_DeleteCommand(DeleteRequest RequestData) : ICommand<Result<bool>>;
    public class DiscountProduct_DeleteCommandHandler : ICommandHandler<DiscountProduct_DeleteCommand, Result<bool>>
    {

        private readonly DataContext _context;

        public DiscountProduct_DeleteCommandHandler(IMapper mapper, DataContext context)
        {
            _context = context;
        }
        public async Task<Result<bool>> Handle(DiscountProduct_DeleteCommand request, CancellationToken cancellationToken)
        {
            if (request.RequestData.Ids == null)
                throw new ApplicationException("Ids not found");

            List<Guid> ids = request.RequestData.Ids.Select(m => Guid.Parse(m)).ToList();
            var query = await _context.DiscountProducts.Where(m => ids.Contains(m.Id)).ToListAsync();
            if (query == null || query.Count == 0) throw new ApplicationException($"Không tìm thấy trong dữ liệu có Id: {string.Join(";", request.RequestData.Ids)}");

			foreach (var item in query)
			{
				item.DeleteFlag = true;
				item.ModifiedDate = DateTime.Now;
				item.ModifiedUser = request.RequestData.ModifiedUser;
			}

			_context.DiscountProducts.UpdateRange(query);

			await _context.SaveChangesAsync(cancellationToken);
            return Result<bool>.Success(true);
        }
    }
}
