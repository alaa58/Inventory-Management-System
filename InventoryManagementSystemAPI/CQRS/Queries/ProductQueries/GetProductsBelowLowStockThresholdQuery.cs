using AutoMapper;
using AutoMapper.QueryableExtensions;
using InventoryManagementSystemAPI.Data;
using InventoryManagementSystemAPI.DTO.ProductDTO;
using InventoryManagementSystemAPI.Models;
using InventoryManagementSystemAPI.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagementSystemAPI.CQRS.Queries.ProductQueries
{
    public class GetProductsBelowLowStockThresholdQuery : IRequest<List<ProductsBelowLowStockThresholdDTO>>
    {
       

    }
    public class GetProductsBelowLowStockThresholdCommandHandler : IRequestHandler<GetProductsBelowLowStockThresholdQuery, List<ProductsBelowLowStockThresholdDTO>>
    {
        private readonly IGeneralRepository<Product> repository;
        private readonly IMapper mapper;

        public GetProductsBelowLowStockThresholdCommandHandler(IGeneralRepository<Product> repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
        public async Task<List<ProductsBelowLowStockThresholdDTO>> Handle(GetProductsBelowLowStockThresholdQuery request, CancellationToken cancellationToken)
        {
            var products = await repository.Get(p => p.Inventories
                .Any(i => i.Quantity < i.LowStockThreshold && i.IsDeleted == false) && p.IsDeleted == false)
                .ProjectTo<ProductsBelowLowStockThresholdDTO>(mapper.ConfigurationProvider)
                .ToListAsync();

            return products;
        }

    }
    
}
