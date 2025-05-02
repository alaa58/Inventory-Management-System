using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryManagementSystemAPI.Models
{
    public class Notification : BaseModel
    {
        public string? Message { get; set; }
        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public virtual Product? Product { get; set; }
    }
}
