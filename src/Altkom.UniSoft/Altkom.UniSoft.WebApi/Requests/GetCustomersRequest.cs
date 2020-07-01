using Alktom.UniSoft.IServices;
using Altkom.UniSoft.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Altkom.UniSoft.WebApi.Requests
{
    public class GetCustomersRequest : IRequest<IEnumerable<Customer>>
    {

    }
}
