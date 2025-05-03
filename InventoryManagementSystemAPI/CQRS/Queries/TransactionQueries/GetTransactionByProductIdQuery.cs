using AutoMapper;
using InventoryManagementSystemAPI.Data;
using InventoryManagementSystemAPI.DTO.InventoryTransactionDTO;
using InventoryManagementSystemAPI.Models;
using MediatR;

namespace InventoryManagementSystemAPI.CQRS.Queries.TransactionQueries
{
    public class GetTransactionByProductIdQuery : IRequest<IEnumerable<TransactionHistoryDTO>>
    {
        public int ProductId { get; set; }
        public TransactionType transactionType { get; set; }
        public GetTransactionByProductIdQuery(int productId, TransactionType transactionType)
        {
            ProductId = productId;
            this.transactionType = transactionType;
        }
    }
    public class GetTransactionByProductIdQueryHandler : IRequestHandler<GetTransactionByProductIdQuery, IEnumerable<TransactionHistoryDTO>>
    {
        private readonly IGeneralRepository<InventoryTransaction> repository;
        private readonly IMapper mapper;

        public GetTransactionByProductIdQueryHandler(IGeneralRepository<InventoryTransaction> repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public Task<IEnumerable<TransactionHistoryDTO>> Handle(GetTransactionByProductIdQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<InventoryTransaction> transactions = repository
                .Get(t => t.ProductId == request.ProductId
                && t.TransactionType == request.transactionType);

            var transactionDTOs = mapper.Map<IEnumerable<TransactionHistoryDTO>>(transactions);
            return Task.FromResult(transactionDTOs);
        }
    }
}