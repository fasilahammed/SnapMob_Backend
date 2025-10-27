using AutoMapper;
using SnapMob_Backend.DTO.ProductDTO;
using SnapMob_Backend.DTOs;
using SnapMob_Backend.Models;

namespace SnapMob_Backend.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // 🧭 Entity → DTO
            CreateMap<Product, ProductDTO>()
                .ForMember(dest => dest.ImageUrls,
                           opt => opt.MapFrom(src => src.Images.Select(i => i.ImageUrl)));

            CreateMap<ProductBrand, ProductBrandDTO>().ReverseMap();



            // 🧭 DTO → Entity
            CreateMap<ProductCreateUpdateDTO, Product>()
                .ForMember(dest => dest.Brand, opt => opt.Ignore())
                .ForMember(dest => dest.Images, opt => opt.Ignore()); // 🛠️ Ignore Images mapping
        }
    }
}
