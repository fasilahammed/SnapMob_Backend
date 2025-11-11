using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SnapMob_Backend.Common;
using SnapMob_Backend.Data;
using SnapMob_Backend.DTOs.OrderDTOs;
using SnapMob_Backend.Enums;
using SnapMob_Backend.Models;
using SnapMob_Backend.Repositories.Interfaces;
using SnapMob_Backend.Services.Interfaces;

namespace SnapMob_Backend.Services.Implementation
{
    public class OrderService : IOrderService
    {
        private readonly IMapper _mapper;
        private readonly ICartRepository _cartRepo;
        private readonly IOrderRepository _orderRepo;
        private readonly IProductRepository _productRepo;
        private readonly AppDbContext _context; // ✅ for transaction

        public OrderService(
            IMapper mapper,
            ICartRepository cartRepo,
            IOrderRepository orderRepo,
            IProductRepository productRepo,
            AppDbContext context)
        {
            _mapper = mapper;
            _cartRepo = cartRepo;
            _orderRepo = orderRepo;
            _productRepo = productRepo;
            _context = context;
        }

        // ✅ CREATE ORDER — Atomic transaction + brand name fix
        public async Task<ApiResponse<OrderDto>> CreateOrderAsync(int userId, CreateOrderDto dto)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var cart = await _cartRepo.GetCartWithItemsByUserIdAsync(userId);
                if (cart == null || !cart.Items.Any())
                    return new ApiResponse<OrderDto>(400, "Cart is empty");

                decimal total = 0;

                foreach (var item in cart.Items)
                {
                    var product = await _productRepo.GetByIdAsync(item.ProductId);
                    if (product == null)
                        return new ApiResponse<OrderDto>(404, $"Product {item.ProductId} not found");

                    if (product.CurrentStock < item.Quantity)
                        return new ApiResponse<OrderDto>(400, $"Not enough stock for {product.Name}");

                    product.CurrentStock -= item.Quantity;
                    _productRepo.UpdateAsync(product);

                    // ✅ Ensure brand & image snapshot
                    item.ImageUrl = product.Images.FirstOrDefault(i => i.IsMain)?.ImageUrl
                                    ?? product.Images.FirstOrDefault()?.ImageUrl
                                    ?? "https://via.placeholder.com/150";
                    item.BrandName = product.Brand?.Name ?? "Unknown Brand";

                    total += product.Price * item.Quantity;
                }

                // ✅ Build Order Snapshot
                var order = new Order
                {
                    UserId = userId,
                    BillingName = dto.FullName,
                    BillingPhone = dto.PhoneNumber,
                    BillingStreet = dto.Street,
                    BillingCity = dto.City,
                    BillingState = dto.State,
                    BillingZip = dto.ZipCode,
                    PaymentMethod = PaymentMethod.CashOnDelivery,
                    PaymentStatus = PaymentStatus.Pending,
                    OrderStatus = OrderStatus.Processing,
                    TotalAmount = total,
                    Items = cart.Items.Select(c => new OrderItem
                    {
                        ProductId = c.ProductId,
                        Name = c.ProductName,
                        BrandName = c.BrandName,
                        Quantity = c.Quantity,
                        Price = c.Price,
                        ImageUrl = c.ImageUrl
                    }).ToList()
                };

                await _orderRepo.AddAsync(order);
                await _orderRepo.SaveChangesAsync();

                await _cartRepo.ClearCartForUserAsync(userId);

                await transaction.CommitAsync();

                var orderDto = _mapper.Map<OrderDto>(order);
                return new ApiResponse<OrderDto>(200, "Order placed successfully", orderDto);
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return new ApiResponse<OrderDto>(500, $"Order creation failed: {ex.Message}");
            }
        }

       

        public async Task<ApiResponse<IEnumerable<OrderDto>>> FilterOrdersAsync(string status, string? search)
        {
            IQueryable<Order> query = _orderRepo.GetQueryable()
                .Include(o => o.User)
                .Include(o => o.Items)
                    .ThenInclude(i => i.Product)
                        .ThenInclude(p => p.Brand);

            if (!string.IsNullOrEmpty(status) && status.ToLower() != "all")
            {
                if (Enum.TryParse<OrderStatus>(status, true, out var parsedStatus))
                {
                    query = query.Where(o => o.OrderStatus == parsedStatus);
                }
            }

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(o =>
                    o.BillingName.Contains(search) ||
                    o.BillingPhone.Contains(search));
            }

            var orders = await query
                .OrderByDescending(o => o.CreatedOn)
                .ToListAsync();

            var mapped = _mapper.Map<IEnumerable<OrderDto>>(orders);
            return new ApiResponse<IEnumerable<OrderDto>>(200, "Filtered orders fetched successfully", mapped);
        }


        // ✅ CUSTOMER — Get all orders
        public async Task<ApiResponse<IEnumerable<OrderDto>>> GetOrdersByUserIdAsync(int userId)
        {
            var orders = await _orderRepo.GetAllAsync(
                predicate: o => o.UserId == userId,
                include: q => q.Include(o => o.Items)
                               .ThenInclude(i => i.Product)
                               .ThenInclude(p => p.Brand)
            );

            if (!orders.Any())
                return new ApiResponse<IEnumerable<OrderDto>>(404, "No orders found");

            var mapped = _mapper.Map<IEnumerable<OrderDto>>(orders);
            return new ApiResponse<IEnumerable<OrderDto>>(200, "Orders fetched successfully", mapped);
        }

        // ✅ CUSTOMER / ADMIN — Get order by ID
        public async Task<ApiResponse<OrderDto>> GetOrderByIdAsync(int userId, int orderId)
        {
            var order = await _orderRepo.GetOrderWithItemsByIdAsync(orderId, userId);
            if (order == null)
                return new ApiResponse<OrderDto>(404, "Order not found");

            var mapped = _mapper.Map<OrderDto>(order);
            return new ApiResponse<OrderDto>(200, "Order fetched successfully", mapped);
        }

        // ✅ ADMIN — Update order status
        public async Task<ApiResponse<OrderDto>> UpdateOrderStatusAsync(int orderId, OrderStatus newStatus)
        {
            var order = await _orderRepo.GetByIdAsync(orderId);
            if (order == null)
                return new ApiResponse<OrderDto>(404, "Order not found");

            if (order.OrderStatus == OrderStatus.Delivered)
                return new ApiResponse<OrderDto>(400, "Delivered order status cannot be changed.");

            order.OrderStatus = newStatus;

            if (order.PaymentMethod == PaymentMethod.CashOnDelivery &&
                newStatus == OrderStatus.Delivered)
            {
                order.PaymentStatus = PaymentStatus.Completed;
            }

            _orderRepo.UpdateAsync(order);
            await _orderRepo.SaveChangesAsync();

            var mapped = _mapper.Map<OrderDto>(order);
            return new ApiResponse<OrderDto>(200, "Order status updated successfully", mapped);
        }

        // ✅ CUSTOMER — Cancel order and restore stock
        public async Task<ApiResponse<string>> CancelOrderAsync(int orderId)
        {
            var order = await _orderRepo.GetByIdAsync(orderId);
            if (order == null)
                return new ApiResponse<string>(404, "Order not found");

            if (order.OrderStatus == OrderStatus.Delivered)
                return new ApiResponse<string>(400, "Delivered orders cannot be cancelled");

            foreach (var item in order.Items)
            {
                var product = await _productRepo.GetByIdAsync(item.ProductId);
                if (product != null)
                {
                    product.CurrentStock += item.Quantity;
                    _productRepo.UpdateAsync(product);
                }
            }

            order.OrderStatus = OrderStatus.Cancelled;
            order.PaymentStatus = PaymentStatus.Refunded;

            _orderRepo.UpdateAsync(order);
            await _productRepo.SaveChangesAsync();
            await _orderRepo.SaveChangesAsync();

            return new ApiResponse<string>(200, "Order cancelled successfully and stock restored");
        }

        // ✅ ADMIN — Dashboard analytics
        public async Task<ApiResponse<object>> GetDashboardStatsAsync(string type)
        {
            var orders = await _orderRepo.GetAllAsync(
                include: q => q.Include(o => o.Items)
                               .ThenInclude(i => i.Product)
            );

            var deliveredOrders = orders
                .Where(o => o.OrderStatus == OrderStatus.Delivered)
                .ToList();

            if (!deliveredOrders.Any())
                return new ApiResponse<object>(404, "No delivered orders found");

            var totalRevenue = deliveredOrders.Sum(o => o.TotalAmount);
            var totalProducts = deliveredOrders.Sum(o => o.Items.Sum(i => i.Quantity));
            var deliveredCount = deliveredOrders.Count;

            var topProducts = deliveredOrders
                .SelectMany(o => o.Items)
                .GroupBy(i => i.ProductId)
                .Select(g => new
                {
                    ProductId = g.Key,
                    ProductName = g.First().Name,
                    Brand = g.First().BrandName,
                    TotalSold = g.Sum(i => i.Quantity),
                    ImageUrl = g.First().ImageUrl
                })
                .OrderByDescending(x => x.TotalSold)
                .Take(5)
                .ToList();

            var data = new
            {
                TotalRevenue = totalRevenue,
                TotalProductsSold = totalProducts,
                DeliveredOrders = deliveredCount,
                Top5SoldProducts = topProducts
            };

            return new ApiResponse<object>(200, "Dashboard stats fetched successfully", data);
        }
    }
}
