using Altkom.UniSoft.Models;
using Altkom.UniSoft.Models.SearchCriteria;
using System;
using System.Collections.Generic;

namespace Alktom.UniSoft.IServices
{
    public interface ICustomerService : IEntityService<Customer>
    {
        Customer Get(string fullname);

        ICollection<Customer> Get(CustomerSearchCriteria criteria);
    }

}
