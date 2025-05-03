using AutoMapper;
using InventoryManagementSystemAPI.Models;
using InventoryManagementSystemAPI.DTO.Product;
using InventoryManagementSystemAPI.DTO.ProductDTO;
public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<AddProductDTO, Product>().ReverseMap();
        CreateMap<Product, UpdateProductDTO>().ReverseMap();
        CreateMap<Product, RemoveProductDTO>().ReverseMap();
        CreateMap<Product, GetProductByIdDTO>().ReverseMap();
        CreateMap<Product, GetAllProductsInTheInventoryDTO>().ReverseMap();
        CreateMap<Product, AllProductsDTO>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
            .ReverseMap();
        CreateMap<Product, ProductsBelowLowStockThresholdDTO>().ReverseMap();
    }
}
