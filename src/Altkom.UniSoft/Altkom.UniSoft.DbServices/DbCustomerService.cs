using Alktom.UniSoft.IServices;
using Altkom.UniSoft.Models;
using Altkom.UniSoft.Models.SearchCriteria;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Transactions;

namespace Altkom.UniSoft.DbServices
{
    public class DbCustomerService : ICustomerService
    {
        private readonly UniSoftContext context;

        public void Add(Customer entity)
        {
            //using (TransactionScope transactionScope = new TransactionScope())
            //{
            //    context.Customers.Add(entity);
            //    context.SaveChanges();

            //    context.Customers.Add(entity);
            //    context.SaveChanges();


            //    transactionScope.Complete();
            //};

            using (var transaction = context.Database.BeginTransaction())
            {                
                context.Customers.Add(entity);
                context.SaveChanges();

                context.Customers.Add(entity);
                context.SaveChanges();

                transaction.Commit();
            }
        }

        public Customer Get(string fullname)
        {
            throw new NotImplementedException();
        }

        public ICollection<Customer> Get(CustomerSearchCriteria criteria)
        {
            IQueryable<Customer> query = context.Customers.AsQueryable();

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

            return query.AsNoTracking().ToList();
        }

        public ICollection<Customer> Get()
        {
            return context.Customers.AsNoTracking().ToList();
        }

        public Customer Get(int id)
        {
            return context.Customers.Find(id);
        }

        public void Remove(int id)
        {
            context.Customers.Remove(Get(id));
            context.SaveChanges();
        }

        public void Update(Customer entity)
        {            
            var customer = Get(entity.Id);

            Trace.WriteLine(context.Entry(customer).State);

            var entities = context.ChangeTracker.Entries();            

            customer.FirstName = "Marcin";
            customer.IsRemoved = true;
            context.SaveChanges();
        }
    }
}
