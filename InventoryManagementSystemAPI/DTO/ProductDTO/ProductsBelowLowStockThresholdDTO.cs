﻿using InventoryManagementSystemAPI.Models;

namespace InventoryManagementSystemAPI.DTO.ProductDTO
{
    public class ProductsBelowLowStockThresholdDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public virtual List<Inventory>? Inventories { get; set; }


    }
}
