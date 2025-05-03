using AutoMapper;
using InventoryManagementSystemAPI.DTO.ProductDTO;
using InventoryManagementSystemAPI.Models;

namespace InventoryManagementSystemAPI.DTO.NotificationDTO
{
    public class NotificationProfile : Profile
    {
        public NotificationProfile()
        {
            CreateMap<AddNotificationDTO, Notification>()
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow))
                .ReverseMap();

            CreateMap<ProductsBelowLowStockThresholdDTO, AddNotificationDTO>()
                .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Message, opt => opt.MapFrom(src =>
                    $"Product '{src.Name}' (ID: {src.Id}) is below low stock threshold. " )).ReverseMap();
        }
    }
}