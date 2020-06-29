using Alktom.UniSoft.IServices;
using Altkom.UniSoft.Models;
using Bogus;
using System;
using System.Collections.Generic;

namespace Altkom.UniSoft.FakeServices
{
    public class FakeCustomerService : ICustomerService
    {
        private readonly ICollection<Customer> customers;

        public FakeCustomerService(Faker<Customer> customerFaker)
        {
            customers = customerFaker.Generate(100);
        }
       
        public void Add(Customer entity)
        {
            throw new NotImplementedException();
        }

        public ICollection<Customer> Get()
        {
            return customers;
        }

        public Customer Get(int id)
        {
            throw new NotImplementedException();
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Customer entity)
        {
            throw new NotImplementedException();
        }
    }
}
