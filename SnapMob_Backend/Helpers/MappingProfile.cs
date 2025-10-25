using AutoMapper;
using SnapMob_Backend.DTO.ProductDTO;
using SnapMob_Backend.Models;

namespace SnapMob_Backend.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Product → ProductDTO mapping
            CreateMap<Product, ProductDTO>()
                .ForMember(dest => dest.BrandName, opt => opt.MapFrom(src => src.Brand.Name))
                .ForMember(dest => dest.MainImageUrl, opt => opt.MapFrom(src =>
                    src.Images.FirstOrDefault(i => i.IsMain).ImageUrl ?? ""));

            
        }
    }
}
