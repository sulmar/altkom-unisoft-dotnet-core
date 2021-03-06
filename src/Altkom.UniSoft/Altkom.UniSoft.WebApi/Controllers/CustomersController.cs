﻿using Alktom.UniSoft.IServices;
using Altkom.UniSoft.Models;
using Altkom.UniSoft.Models.SearchCriteria;
using Altkom.UniSoft.WebApi.Events;
using Altkom.UniSoft.WebApi.Handlers;
using Altkom.UniSoft.WebApi.Requests;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Altkom.UniSoft.WebApi.Controllers
{
    // dotnet add package MediatR

    [Authorize(Roles="developer, trainer")]
    // [RoutePrefix("api/customers")]
    [Route("api/customers")]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService customerService;
        private readonly ISenderService senderService;

        private readonly IMediator mediator;

        private readonly ILogger<CustomersController> logger;

        // private readonly IProductService productService;

        public CustomersController(
            ICustomerService customerService,
            ISenderService senderService,
            ILogger<CustomersController> logger,
            IMediator mediator)
        {
            this.customerService = customerService;
            this.senderService = senderService;
            this.logger = logger;
            this.mediator = mediator;
            //this.productService = productService;
        }

        //[HttpGet]
        //public ICollection<Customer> Get()
        //{
        //    var customers = customerService.Get();

        //    return customers;
        //}


        [HttpGet]
        public async Task<IActionResult> Get()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return BadRequest();
            }

            // var customers = customerService.Get();

            var customers = await mediator.Send(new GetCustomersRequest());

            //if (!User.IsInRole("Boss"))
            //{
            //    customers = customers.Where(c =>)
            //}

            // logger.LogInformation($"Pobrano {customers.Count} klientów.");

            return Ok(customers);
        }

        public async Task<IActionResult> Upload()
        {
            return Accepted();
        }


        //[HttpGet("{id}")]
        //public Customer Get(int id)
        //{
        //    var customer = customerService.Get(id);

        //    return customer;
        //}

        // [HttpGet("{id:int}", Name = "GetById")]
        [HttpGet(Name = "GetById")]
        //[Route("{id:int}")]
        [Route("{id:int}.{format?}"), FormatFilter]
        public async Task<IActionResult> Get(int id)
        {
            var customer = customerService.Get(id);

            if (customer == null)
                return NotFound();

            return Ok(customer);
        }

        // api/customers/MorrisHarber


        //[HttpGet("{fullname}")]
        //public async Task<IActionResult> GetByFullname(string fullname)
        //{
        //    var customer = customerService.Get(fullname);

        //    if (customer == null)
        //        return NotFound();

        //    return Ok(customer);
        //}

        //[HttpGet]
        //public async Task<IActionResult> Get(string country, string city, string street)
        //{
        //    return Ok();
        //}

        //[HttpGet]
        //public async Task<IActionResult> Get([FromQuery] CustomerSearchCriteria criteria)
        //{
        //    var customers = customerService.Get(criteria);

        //    return Ok(customers);
        //}

        //[HttpGet]
        //public async Task<IActionResult> Get(
        //    [Required][Range(-90, 90)] double lat,
        //    [Required][Range(-180, 180)] double lng)
        //{
        //    return Ok();
        //}

        //  GET api/customers/{id}/invoices"
        //  GET api/customers/{id}/invoices/overdue"
        //  GET api/customers/{id}/account"
        //  GET api/customers/{id}/payments"
        //  GET api/customers/{id}/payments/{paymentId}"

        //  GET api/payments/{paymentId}"

        [AllowAnonymous]
        [HttpGet("{customerId}/products")]
        public async Task<IActionResult> GetProducts([FromServices] IProductService productService, int customerId)
        {
            var products = productService.GetByCustomer(customerId);

            return Ok(products);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Customer customer)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            //customerService.Add(customer);
            //senderService.Send(customer);

            await mediator.Publish(new AddCustomerEvent(customer));

            // return Created($"api/customers/{customer.Id}", customer);

            return CreatedAtRoute("GetById", new { id = customer.Id }, customer);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Customer customer)
        {
            try
            {
                if (id != customer.Id)
                {
                    return BadRequest();
                }

                customer = customerService.Get(id);

                if (customer == null)
                    return NotFound();

                customerService.Update(customer);

                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        // JSON Patch http://jsonpatch.com/

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(int id, string lastname)
        {
            Customer customer = customerService.Get(id);

            if (customer == null)
                return NotFound();

            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            Customer customer = customerService.Get(id);

            if (customer == null)
                return NotFound();

            customerService.Remove(id);

            return NoContent();
        }

        [HttpHead("{id}")]
        public async Task<IActionResult> Head(int id)
        {
            Customer customer = customerService.Get(id);

            if (customer == null)
                return NotFound();

            return Ok();

        }

    }
}
