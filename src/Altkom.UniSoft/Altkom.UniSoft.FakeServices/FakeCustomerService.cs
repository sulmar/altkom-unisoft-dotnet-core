using Alktom.UniSoft.IServices;
using Altkom.UniSoft.Models;
using Altkom.UniSoft.Models.SearchCriteria;
using Bogus;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace Altkom.UniSoft.FakeServices
{

    // Wzorzec opcji
    
    public class FakeCustomerServiceOptions
    {
        public int Count { get; set; }
    }

    // dotnet add package Microsoft.Extensions.Options
    public class FakeCustomerService : ICustomerService
    {
        private readonly ICollection<Customer> customers;

        public FakeCustomerService(Faker<Customer> customerFaker, IOptions<FakeCustomerServiceOptions> options)
        {
            customers = customerFaker.Generate(options.Value.Count);
        }
       
        public void Add(Customer entity)
        {
            var lastId = customers.Max(c => c.Id);
            entity.Id = ++lastId;
            customers.Add(entity);
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
            customers.Remove(Get(id));
        }

        public void Update(Customer entity)
        {
            Remove(entity.Id);
            Add(entity);            
        }
    }
}
