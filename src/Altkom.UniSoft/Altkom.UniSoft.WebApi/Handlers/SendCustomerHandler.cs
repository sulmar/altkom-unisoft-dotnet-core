using Alktom.UniSoft.IServices;
using Altkom.UniSoft.WebApi.Events;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Altkom.UniSoft.WebApi.Handlers
{
    public class SendCustomerHandler : INotificationHandler<AddCustomerEvent>
    {
        private readonly ISenderService senderService;

        public SendCustomerHandler(ISenderService senderService)
        {
            this.senderService = senderService;
        }

        public Task Handle(AddCustomerEvent notification, CancellationToken cancellationToken)
        {
            senderService.Send(notification.Customer);

            return Task.CompletedTask;
        }
    }
}
