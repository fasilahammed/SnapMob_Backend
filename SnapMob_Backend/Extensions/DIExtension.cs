using SnapMob_Backend.Common;
using SnapMob_Backend.Repositories.implementation;
using SnapMob_Backend.Repositories.Implementation;
using SnapMob_Backend.Repositories.interfaces;
using SnapMob_Backend.Repositories.Interfaces;
using SnapMob_Backend.Services.implementation;
using SnapMob_Backend.Services.Implementation;
using SnapMob_Backend.Services.Interfaces;
using SnapMob_Backend.Services.Services.implementation;
using SnapMob_Backend.Services.Services.interfaces;

namespace SnapMob_Backend.Extensions
{
    public static class DIExtension
    {
        public static IServiceCollection AddAppServices(this IServiceCollection services)
        {
            //  Repositories
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductBrandRepository, ProductBrandRepository>();
            services.AddScoped<IWishlistRepository, WishlistRepository>();
            services.AddScoped<ICartRepository, CartRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();

            //  Services
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IProductBrandService, ProductBrandService>();
            services.AddScoped<IWishlistService, WishlistService>();
            services.AddScoped<ICartService, CartService>();
            services.AddScoped<IPaymentService, PaymentService>();
            services.AddScoped<CloudinaryService>();

            //  AutoMapper
            services.AddAutoMapper(typeof(MappingProfile));

            return services;
        }
    }
}
