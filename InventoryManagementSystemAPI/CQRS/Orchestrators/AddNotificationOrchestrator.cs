using AutoMapper;
using InventoryManagementSystemAPI.CQRS.Commands.NotoficationCommand;
using InventoryManagementSystemAPI.CQRS.Queries.ProductQueries;
using InventoryManagementSystemAPI.DTO.NotificationDTO;
using MediatR;

public class AddNotificationOrchestrator : IRequest<List<AddNotificationDTO>>
{
    public AddNotificationOrchestrator() { }
}

public class AddNotificationOrchestratorHandler : IRequestHandler<AddNotificationOrchestrator, List<AddNotificationDTO>>
{
    private readonly IMediator mediator;
    private readonly IMapper mapper;

    public AddNotificationOrchestratorHandler(IMediator mediator, IMapper mapper)
    {
        this.mediator = mediator;
        this.mapper = mapper;
    }

    public async Task<List<AddNotificationDTO>> Handle(AddNotificationOrchestrator request, CancellationToken cancellationToken)
    {
        var lowStockProducts = await mediator.Send(new GetProductsBelowLowStockThresholdQuery());

        var results = new List<AddNotificationDTO>();

        foreach (var product in lowStockProducts)
        {
            var notification = mapper.Map<AddNotificationDTO>(product);

            var result = await mediator.Send(new AddNotificationCommand { notificationDTO = notification});
            results.Add(result);
        }

        return results;
    }
}
