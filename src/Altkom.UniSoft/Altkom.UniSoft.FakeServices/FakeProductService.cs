using Alktom.UniSoft.IServices;
using Altkom.UniSoft.Models;
using System;
using System.Collections.Generic;

namespace Altkom.UniSoft.FakeServices
{
    public class FakeProductService : IProductService
    {
        public void Add(Product entity)
        {
            throw new NotImplementedException();
        }

        public ICollection<Product> Get()
        {
            throw new NotImplementedException();
        }

        public Product Get(int id)
        {
            throw new NotImplementedException();
        }

        public ICollection<Product> GetByCustomer(int customerId)
        {
            throw new NotImplementedException();
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Product entity)
        {
            throw new NotImplementedException();
        }
    }
}
