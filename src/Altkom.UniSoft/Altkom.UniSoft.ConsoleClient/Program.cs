using Alktom.UniSoft.IServices;
using Altkom.UniSoft.Models;
using Altkom.UniSoft.RestApiServices;
using Bogus;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Altkom.UniSoft.ConsoleClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            // await AddCustomerTest();

            // await GetCustomersTest();

            StateMachineTest();


            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }

        private static void StateMachineTest()
        {
            LampProxy lamp = new LampProxy();

            Console.WriteLine(lamp.Graph);

            Console.WriteLine(lamp.Status);

            lamp.Push();

            Console.WriteLine(lamp.Status);

            lamp.Temp = 51;

            Console.WriteLine(lamp.Status);

            lamp.Push();

            Console.WriteLine(lamp.Status);

            lamp.Push();

            Console.WriteLine(lamp.Status);
        }

        private static async Task AddCustomerTest()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:5001");

            // dotnet add package Bogus
            Customer customer =  new Faker<Customer>()
                .RuleFor(p => p.FirstName, f => f.Person.FirstName)
                .RuleFor(p => p.LastName, f => f.Person.LastName)
                .RuleFor(p => p.Gender, f => (Gender)f.Person.Gender)
                .RuleFor(p => p.Email, (f, c) => $"{c.FirstName}.{c.LastName}@unisoft.pl")
                .Generate();

            ICustomerServiceAsync customerService = new RestApiCustomerService(client);
            await customerService.AddAsync(customer);
        }

        private static async Task GetCustomersTest()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:5001");

            ICustomerServiceAsync customerService = new RestApiCustomerService(client);            

            IEnumerable<Customer> customers = await customerService.GetAsync();

            foreach (var customer in customers)
            {
                Console.WriteLine(customer.FullName);
            }
        }
    }
}
