using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryManagementSystemAPI.DTO.NotificationDTO
{
    public class AddNotificationDTO
    {
        public string? Message { get; set; }
        public int ProductId { get; set; }
    }
}
