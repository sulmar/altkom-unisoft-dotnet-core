using Alktom.UniSoft.IServices;
using Altkom.UniSoft.Models;
using Altkom.UniSoft.WebApi.Requests;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Altkom.UniSoft.WebApi.Handlers
{
    public class GetCustomersHandler : IRequestHandler<GetCustomersRequest, IEnumerable<Customer>>
    {
        private readonly ICustomerService customerService;

        public GetCustomersHandler(ICustomerService customerService)
        {
            this.customerService = customerService;
        }

        public Task<IEnumerable<Customer>> Handle(GetCustomersRequest request, CancellationToken cancellationToken)
        {
            var customers = customerService.Get();
            return Task.FromResult(customers.AsEnumerable()); 
        }
    }
}
