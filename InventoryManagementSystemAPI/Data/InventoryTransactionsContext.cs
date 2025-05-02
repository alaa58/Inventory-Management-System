using InventoryManagementSystemAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagementSystemAPI.Data
{
    public class InventoryTransactionsContext:IdentityDbContext<ApplicationUser> 
    {
        
        public InventoryTransactionsContext(DbContextOptions<InventoryTransactionsContext> options) : base(options)
        {
        }
        public DbSet<InventoryTransaction> InventoryTransactions { get; set; } = null!;
        public DbSet<Product> Products { get; set; } = null!;
        public DbSet<Warehouse> Warehouses { get; set; } = null!;
        public DbSet<Notification> Notifications { get; set; } = null!;
       
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); 

            modelBuilder.Entity<IdentityUserLogin<string>>(b =>
            {
                b.HasKey(l => new { l.LoginProvider, l.ProviderKey });
            });

            modelBuilder.Entity<IdentityUserRole<string>>(b =>
            {
                b.HasKey(r => new { r.UserId, r.RoleId });
            });

            modelBuilder.Entity<IdentityUserToken<string>>(b =>
            {
                b.HasKey(t => new { t.UserId, t.LoginProvider, t.Name });
            });

            modelBuilder.Entity<InventoryTransaction>()
                .HasOne(t => t.FromWarehouse)
                .WithMany(t => t.OutgoingInventoryTransactions)
                .HasForeignKey(t => t.FromWarehouseId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<InventoryTransaction>()
                .HasOne(t => t.ToWarehouse)
                .WithMany(t => t.IncomingInventoryTransactions)
                .HasForeignKey(t => t.ToWarehouseId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
