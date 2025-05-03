using InventoryManagementSystemAPI.CQRS.Commands.NotoficationCommand;
using InventoryManagementSystemAPI.DTO.NotificationDTO;
using MediatR;

namespace InventoryManagementSystemAPI.Jobs
{
    public class NotificationJobRunner
    {
        private readonly IMediator _mediator;

        public NotificationJobRunner(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task Run()
        {
            var notificationDto = new AddNotificationDTO
            {
                Message = "Low stock alert"
            };

            var command = new AddNotificationCommand(notificationDto);
            await _mediator.Send(command);
        }
    }
}
