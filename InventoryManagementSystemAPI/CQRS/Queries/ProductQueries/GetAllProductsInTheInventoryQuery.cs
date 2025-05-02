using System.Collections.Generic;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using InventoryManagementSystemAPI.Data;
using InventoryManagementSystemAPI.DTO.ProductDTO;
using InventoryManagementSystemAPI.Models;
using MediatR;

namespace InventoryManagementSystemAPI.CQRS.Queries.ProductQueries
{
    public class GetAllProductsInTheInventoryQuery : IRequest<IEnumerable<GetAllProductsInTheInventoryDTO>>
    {
        public readonly GetAllProductsInTheInventoryDTO getAllProductsInTheInventoryDTO;

        public GetAllProductsInTheInventoryQuery(GetAllProductsInTheInventoryDTO getAllProductsInTheInventoryDTO)
        {
            this.getAllProductsInTheInventoryDTO = getAllProductsInTheInventoryDTO;
        }
    }
    public class GetAllProductsInTheInventoryQueryHandler : IRequestHandler<GetAllProductsInTheInventoryQuery, IEnumerable<GetAllProductsInTheInventoryDTO>>
    {
        private readonly IGeneralRepository<Product> repository;
        private readonly IMapper mapper;

        public GetAllProductsInTheInventoryQueryHandler(IGeneralRepository<Product> repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
        public async Task<IEnumerable<GetAllProductsInTheInventoryDTO>> Handle(GetAllProductsInTheInventoryQuery request, CancellationToken cancellationToken)
        {
            var products = repository.Get(p => p.Inventories
                .Any(i => i.IsDeleted == false))
                .ProjectTo<GetAllProductsInTheInventoryDTO>(mapper.ConfigurationProvider)
                .ToList();
            return await Task.FromResult(products);
        }
    }
}
