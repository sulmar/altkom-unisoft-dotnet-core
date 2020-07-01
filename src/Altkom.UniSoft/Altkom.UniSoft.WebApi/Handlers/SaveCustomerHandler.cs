using Alktom.UniSoft.IServices;
using Altkom.UniSoft.WebApi.Events;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Altkom.UniSoft.WebApi.Handlers
{
    public class SaveCustomerHandler : INotificationHandler<AddCustomerEvent>
    {
        private readonly ICustomerService customerService;

        public SaveCustomerHandler(ICustomerService customerService)
        {
            this.customerService = customerService;
        }

        public Task Handle(AddCustomerEvent notification, CancellationToken cancellationToken)
        {
            customerService.Add(notification.Customer);

            return Task.CompletedTask;
        }
    }
}
