using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryManagementSystemAPI.Models
{
    public class Notification
    {
        public int Id { get; set; }
        public string? Message { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsDeleted { get; set; }
        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public virtual Products Product { get; set; } = new Products();
    }
}
