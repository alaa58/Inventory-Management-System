using AutoMapper;
using InventoryManagementSystemAPI.Models;

namespace InventoryManagementSystemAPI.DTO.InventoryTransactionDTO
{
    public class InventoryTransactionProfile: Profile
    {
        public InventoryTransactionProfile()
        {
            CreateMap<InventoryTransactionAddStockDTO, InventoryTransaction>().ReverseMap();
            CreateMap<InventoryTransaction, InventoryTransactionAddStockDTO>().ReverseMap();
            CreateMap<InventoryTransaction, InventoryTransactionRemoveStockDTO>().ReverseMap();
        }
    }
}
