using Alktom.UniSoft.IServices;
using Altkom.UniSoft.Models;
using Altkom.UniSoft.Models.SearchCriteria;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Altkom.UniSoft.RestApiServices
{
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

            HttpContent content = new StringContent(json);
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
