using Alktom.UniSoft.IServices;
using Altkom.UniSoft.Models;
using Altkom.UniSoft.Models.SearchCriteria;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Altkom.UniSoft.DapperServices
{

    // dotnet add package Dapper
    public class DbCustomerService : ICustomerService
    {
        private readonly IDbConnection connection;

        public DbCustomerService(IDbConnection connection)
        {
            this.connection = connection;
        }

        public void Add(Customer entity)
        {
            throw new NotImplementedException();
        }

        public Customer Get(string fullname)
        {
            throw new NotImplementedException();
        }

        public ICollection<Customer> Get(CustomerSearchCriteria criteria)
        {
            throw new NotImplementedException();
        }

        public ICollection<Customer> Get()
        {
            string sql = "SELECT * FROM dbo.Customers";

            var customers = connection.Query<Customer>(sql).ToList();

            return customers;
        }

        public Customer Get(int id)
        {
            string sql = "SELECT * FROM dbo.Customers WHERE Id = @id";

            var customer = connection.QuerySingleOrDefault<Customer>(sql, new { @id = id });

            return customer;
        }

        public void Remove(int id)
        {
            string sql = "DELETE FROM dbo.Customers WHERE Id = @id";

            connection.Execute(sql, new { @id = id });
        }

        public void Update(Customer entity)
        {
            throw new NotImplementedException();
        }
    }
}
