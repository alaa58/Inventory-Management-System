using AutoMapper;
using InventoryManagementSystemAPI.Data;
using InventoryManagementSystemAPI.DTO.NotificationDTO;
using InventoryManagementSystemAPI.Models;
using MediatR;

namespace InventoryManagementSystemAPI.CQRS.Commands.NotoficationCommand
{
    public class AddNotificationCommand : IRequest<AddNotificationDTO>
    {
        public AddNotificationDTO notificationDTO { get; set; }
        public AddNotificationCommand() { }
        public AddNotificationCommand(AddNotificationDTO notificationDTO)
        {
            this.notificationDTO = notificationDTO;
        }
    }
    public class AddNotificationCommandHandler : IRequestHandler<AddNotificationCommand, AddNotificationDTO>
    {
        private readonly IGeneralRepository<Notification> repository;
        private readonly IMapper mapper;
        public AddNotificationCommandHandler(IGeneralRepository<Notification> repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
        public async Task<AddNotificationDTO> Handle(AddNotificationCommand request, CancellationToken cancellationToken)
        {
            var notification = mapper.Map<Notification>(request.notificationDTO);

            repository.Add(notification);
            await repository.SaveChangesAsync();

            return mapper.Map<AddNotificationDTO>(notification);
        }
    }
}
