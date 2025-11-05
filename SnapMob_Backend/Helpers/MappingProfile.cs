using AutoMapper;
using SnapMob_Backend.DTO;
using SnapMob_Backend.DTO.CartDTO;
using SnapMob_Backend.DTOs.OrderDTOs;

using SnapMob_Backend.DTO.ProductDTO;
using SnapMob_Backend.DTOs;
using SnapMob_Backend.DTOs.UserDTOs;
using SnapMob_Backend.Models;

namespace SnapMob_Backend.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductDTO>()
                .ForMember(dest => dest.ImageUrls,
                           opt => opt.MapFrom(src => src.Images.Select(i => i.ImageUrl)));

            CreateMap<ProductBrand, ProductBrandDTO>().ReverseMap();

            CreateMap<ProductCreateDTO, Product>()
                .ForMember(dest => dest.Brand, opt => opt.Ignore())
                .ForMember(dest => dest.Images, opt => opt.Ignore());

            var updateMap = CreateMap<ProductUpdateDTO, Product>();
            updateMap.ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
            updateMap.ForMember(dest => dest.Brand, opt => opt.Ignore());
            updateMap.ForMember(dest => dest.Images, opt => opt.Ignore());

            CreateMap<Wishlist, WishlistDTO>()
                 .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.Product.Id))
                 .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name))
                 .ForMember(dest => dest.BrandName, opt => opt.MapFrom(src => src.Product.Brand.Name))
                 .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Product.Price))
                 .ForMember(dest => dest.ImageUrls, opt => opt.MapFrom(src => src.Product.Images.Select(i => i.ImageUrl)));

            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<UserUpdateDTO, User>();
            CreateMap<User, UserDTO>()
                 .ForMember(dest => dest.CreatedOn,
                     opt => opt.MapFrom(src => src.CreatedOn.ToString("yyyy-MM-dd")));

            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name));

            CreateMap<Order, OrderDto>()
                .ForMember(dest => dest.PaymentMethod, opt => opt.MapFrom(src => src.PaymentMethod.ToString()))
                .ForMember(dest => dest.PaymentStatus, opt => opt.MapFrom(src => src.PaymentStatus.ToString()))
                .ForMember(dest => dest.OrderStatus, opt => opt.MapFrom(src => src.OrderStatus.ToString()))
                .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items));
        }
    }
}
