using AutoMapper;
using InventoryManagementSystemAPI.Models;

namespace InventoryManagementSystemAPI.DTO.InventoryDTO
{
    public class InventoryProfile: Profile
    {
        public InventoryProfile() 
        {
            CreateMap<AddStockDTO, Inventory>()
                .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductId))
                .ForMember(dest => dest.WarehouseId, opt => opt.MapFrom(src => src.WarehouseId))
                .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity))
                .ReverseMap();
            CreateMap<Inventory, AddStockDTO>()
                .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductId))
                .ForMember(dest => dest.WarehouseId, opt => opt.MapFrom(src => src.WarehouseId))
                .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity))
                .ReverseMap();
            CreateMap<Inventory, RemoveStockDTO>().ReverseMap();

        }
    }
}
