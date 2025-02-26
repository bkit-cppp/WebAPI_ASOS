using BuildingBlock.Grpc.Protos;
using BuildingBlock.Messaging.Abstractions;
using BuildingBlock.Messaging.Events;
using Ordering.API.Features.OrderFeature.Dto;

namespace Ordering.API.Features.OrderFeature.Commands;

public record OrderCheckoutV2Command(Guid user, CheckoutUrlRequest requestData) : ICommand<Result<OrderDto>>;
public class OrderCheckoutV2CommandHandler : ICommandHandler<OrderCheckoutV2Command, Result<OrderDto>>
{
    private readonly DataContext _context;
    private readonly BasketGrpcService _basketService;
    private readonly IOrderHistoryService _historyService;
    private readonly IMapper _mapper;
    private readonly IEventBus _eventBus;
    public OrderCheckoutV2CommandHandler(
        DataContext context,
        BasketGrpcService basketService,
        IOrderHistoryService historyService,
        IEventBus eventBus,
        IMapper mapper)
    {
        _context = context;
        _basketService = basketService;
        _historyService = historyService;
        _eventBus = eventBus;
        _mapper = mapper;
    }
    public async Task<Result<OrderDto>> Handle(OrderCheckoutV2Command request, CancellationToken cancellationToken)
    {
        var cart = await _basketService.GetCartAsync(request.user);

        var status = await _context.OrderStatus.FindAsync(OrderStatusConstant.Placed);
        if (status == null)
        {
            throw new ApplicationException($"Status '{OrderStatusConstant.Placed}' not found");
        }
        var order = new Order()
        {
            UserId = request.user,
            Total = (decimal)cart.Total,
            StatusId = status.Id,
            Status = status,
            ReceiverName = request.requestData.ReceiverName,
            Email = request.requestData.Email,
            Address = request.requestData.Address,
            Phone = request.requestData.Phone,
            ModifiedUser = request.user,
            CreatedUser = request.user
        };

        if (!string.IsNullOrEmpty(cart.DiscountId))
        {
            order.DiscountId = Guid.Parse(cart.DiscountId);
        }
        order.DiscountPrice = (decimal)cart.DiscountPrice;
        order.BasePrice = (decimal)cart.BasePrice;
        order.SubPrice = (decimal)cart.SubPrice;
        order.PointUsed = cart.PointUsed;
        order.ModifiedDate = DateTime.Now;

        var variations = new List<VariationCheckout>();
        foreach (var item in cart.Items)
        {
            CreateOrderItem(order, item);

            VariationCheckout variation = new VariationCheckout()
            {
                VariationId = Guid.Parse(item.VariationId),
                Quantity = item.Quantity
            };
            variations.Add(variation);
        }

        _context.Orders.Add(order);

        int rows = await _context.SaveChangesAsync();
        if (rows > 0)
        {
            await _historyService.CreateHistory(order, "Pending", status.Name);
            await PublishEvent(order, variations);
        }

        return Result<OrderDto>.Success(_mapper.Map<OrderDto>(order));
    }

    private void CreateOrderItem(Order order, CartItem item)
    {
        var price = item.IsSale == true ? item.SalePrice : item.OriginalPrice;
        OrderItem orderItem = new OrderItem()
        {
            OrderId = order.Id,
            ProductId = Guid.Parse(item.ProductId),
            ProductItemId = Guid.Parse(item.ProductItemId),
            VariationId = Guid.Parse(item.VariationId),
            Name = item.Name,
            Brand = item.Brand,
            Category = item.Category,
            Size = item.Size,
            Color = item.Color,
            Quantity = item.Quantity,
            Image = item.Image,
            Price = (decimal)price,
            Stock = (decimal)item.Stock,
            Amount = (decimal)item.FinalPrice,
            CreatedDate = order.CreatedDate,
            ModifiedDate = order.ModifiedDate,
            CreatedUser = order.CreatedUser,
            ModifiedUser = order.ModifiedUser,
        };
        _context.OrderItems.Add(orderItem);
    }

    private async Task PublishEvent(Order order, List<VariationCheckout> variations)
    {
        await _eventBus.PublishAsync(new OrderCheckoutEvent()
        {
            OrderId = order.Id,
            DiscountId = order.DiscountId,
            PointUsed = order.PointUsed,
            UserId = order.UserId,
            Variations = variations
        });
    }
}