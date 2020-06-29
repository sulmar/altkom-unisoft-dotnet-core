using Alktom.UniSoft.IServices;
using Altkom.UniSoft.Models;
using Altkom.UniSoft.Models.SearchCriteria;
using Bogus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

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
            return customers.SingleOrDefault(p => p.Id == id);
        }

        public Customer Get(string fullname)
        {
            return customers.SingleOrDefault(c => c.FullName == fullname);
        }

        public ICollection<Customer> Get(CustomerSearchCriteria criteria)
        {
            IQueryable<Customer> query = customers.AsQueryable();

            if (!string.IsNullOrEmpty(criteria.Country))
            {
                query = query.Where(c => c.Country == criteria.Country);
            }

            if (!string.IsNullOrEmpty(criteria.City))
            {
                query = query.Where(c => c.City == criteria.City);
            }

            if (!string.IsNullOrEmpty(criteria.Street))
            {
                query = query.Where(c => c.Street == criteria.Street);
            }

            return query.ToList();
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
