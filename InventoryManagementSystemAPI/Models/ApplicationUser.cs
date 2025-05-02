using Microsoft.AspNetCore.Identity;

namespace InventoryManagementSystemAPI.Models
{
    public class ApplicationUser:IdentityUser
    {
        List<InventoryTransaction>? InventoryTransactions { get; set; }
    }
}
