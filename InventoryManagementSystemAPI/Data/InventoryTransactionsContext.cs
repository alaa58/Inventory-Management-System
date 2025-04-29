using InventoryManagementSystemAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagementSystemAPI.Data
{
    public class InventoryTransactionsContext:DbContext 
    {
        public InventoryTransactionsContext(DbContextOptions<InventoryTransactionsContext> options) : base(options)
        {
        }
        public DbSet<InventoryTransactions> InventoryTransactions { get; set; } = null!;
        public DbSet<Products> Products { get; set; } = null!;
        public DbSet<Warehouse> Warehouses { get; set; } = null!;
        public DbSet<Notification> Notifications { get; set; } = null!;
       
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           modelBuilder.Entity<InventoryTransactions>()
                .HasOne(t => t.FromWarehouse)
                .WithMany(t => t.OutgoingInventoryTransactions)
                .HasForeignKey(t => t.FromWarehouseId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<InventoryTransactions>()
                .HasOne(t => t.ToWarehouse)
                .WithMany(t => t.IncomingInventoryTransactions)
                .HasForeignKey(t => t.ToWarehouseId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
