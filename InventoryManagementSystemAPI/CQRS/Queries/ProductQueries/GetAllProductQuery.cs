using AutoMapper;
using AutoMapper.QueryableExtensions;
using InventoryManagementSystemAPI.Data;
using InventoryManagementSystemAPI.DTO.ProductDTO;
using InventoryManagementSystemAPI.Models;
using MediatR;

namespace InventoryManagementSystemAPI.CQRS.Queries.ProductQueries
{
    public class GetAllProductQuery : IRequest<IEnumerable<AllProductsDTO>>
    {
        public readonly AllProductsDTO allProductsDTO;

        public GetAllProductQuery(AllProductsDTO allProductsDTO)
        {
            this.allProductsDTO = allProductsDTO;
        }

    }
    public class GetAllProductCommandHandler : IRequestHandler<GetAllProductQuery, IEnumerable<AllProductsDTO>>
    {
        private readonly IGeneralRepository<Product> repository;
        private readonly IMapper mapper;

        public GetAllProductCommandHandler(IGeneralRepository<Product> repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
        public async Task<IEnumerable<AllProductsDTO>> Handle(GetAllProductQuery request, CancellationToken cancellationToken)
        {
            var products = repository.Get(p => p.IsDeleted == false)
                .ProjectTo<AllProductsDTO>(mapper.ConfigurationProvider)
                .ToList();

            return await Task.FromResult(products);
        }

    }
}
