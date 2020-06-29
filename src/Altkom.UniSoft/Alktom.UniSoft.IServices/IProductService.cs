using Altkom.UniSoft.Models;
using System.Collections;
using System.Collections.Generic;

namespace Alktom.UniSoft.IServices
{
    public interface IProductService : IEntityService<Product>
    {
        ICollection<Product> GetByCustomer(int customerId);
    }
}

