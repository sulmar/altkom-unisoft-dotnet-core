using Alktom.UniSoft.IServices;
using Altkom.UniSoft.Models;
using Altkom.UniSoft.Models.SearchCriteria;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Altkom.UniSoft.RestApiServices
{
    public class RestApiCustomerService2 : RestApiEntityService<Customer>
    {
        public RestApiCustomerService2(HttpClient client) : base(client)
        {
        }
    }

    public class RestApiEntityService<T> 
    {
        private readonly HttpClient client;

        public RestApiEntityService(HttpClient client)
        {
            this.client = client;
        }

        public async Task AddAsync(T entity)
        {
            var json = JsonConvert.SerializeObject(entity);

            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PostAsync("api/customers", content);

            response.EnsureSuccessStatusCode();

            var jsonResponse = await response.Content.ReadAsStringAsync();

            entity = JsonConvert.DeserializeObject<T>(jsonResponse);
        }

        public async Task<ICollection<T>> GetAsync()
        {
            var json = await client.GetStringAsync("api/customers");

            var entities = JsonConvert.DeserializeObject<ICollection<T>>(json);

            return entities;
        }

        public async Task<T> GetAsync(int id)
        {
            var json = await client.GetStringAsync($"api/customers/{id}");

            var entity = JsonConvert.DeserializeObject<T>(json);

            return entity;
        }

        public Task RemoveAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Customer entity)
        {
            throw new NotImplementedException();
        }
    }

    public class RestApiCustomerService : ICustomerServiceAsync
    {        
        private readonly HttpClient client;

        public RestApiCustomerService(HttpClient client)
        {
            this.client = client;
        }

        public async Task AddAsync(Customer entity)
        {
            var json = JsonConvert.SerializeObject(entity);

            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PostAsync("api/customers", content);

            response.EnsureSuccessStatusCode();

            var jsonResponse = await response.Content.ReadAsStringAsync();

            entity = JsonConvert.DeserializeObject<Customer>(jsonResponse);
        }

        public async Task<ICollection<Customer>> GetAsync()
        {
            var json = await client.GetStringAsync("api/customers");

            var customers = JsonConvert.DeserializeObject<ICollection<Customer>>(json);

            return customers;
        }

        public async Task<Customer> GetAsync(int id)
        {
            var json = await client.GetStringAsync($"api/customers/{id}");

            var customer = JsonConvert.DeserializeObject<Customer>(json);

            return customer;
        }

        public Task RemoveAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Customer entity)
        {
            throw new NotImplementedException();
        }
    }
}
