using AutoMapper;
using InventoryManagementSystemAPI.Data;
using InventoryManagementSystemAPI.DTO.ProductDTO;
using InventoryManagementSystemAPI.Models;
using InventoryManagementSystemAPI.Services;
using MediatR;

namespace InventoryManagementSystemAPI.CQRS.Queries.ProductQueries
{
    public class GetProductByIdQuery : IRequest<GetProductByIdDTO>
    {
        public int Id { get; set; }

        public readonly GetProductByIdDTO getProductByIdDTO;

        public GetProductByIdQuery(int id, GetProductByIdDTO getProductByIdDTO)
        {
            Id = id;
            this.getProductByIdDTO = getProductByIdDTO;
        }
    }
    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, GetProductByIdDTO>
    {
        private readonly IGeneralRepository<Product> repository;
        private readonly IMapper mapper;

        public GetProductByIdQueryHandler(IGeneralRepository<Product> repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
        public Task<GetProductByIdDTO> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var product = repository.Get(p => p.ID == request.Id && p.IsDeleted == false).FirstOrDefault();
            if (product == null)
            {
                throw new Exception("Product not found");
            }
            GetProductByIdDTO gettenProduct = mapper.Map<GetProductByIdDTO>(product);

            return Task.FromResult(gettenProduct);
        }
    }
}
