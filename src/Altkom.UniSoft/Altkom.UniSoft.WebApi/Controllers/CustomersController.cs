using Alktom.UniSoft.IServices;
using Altkom.UniSoft.Models;
using Altkom.UniSoft.Models.SearchCriteria;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Altkom.UniSoft.WebApi.Controllers
{
    // [RoutePrefix("api/customers")]
    [Route("api/customers")]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService customerService;

        public CustomersController(ICustomerService customerService)
        {
            this.customerService = customerService;
        }

        //[HttpGet]
        //public ICollection<Customer> Get()
        //{
        //    var customers = customerService.Get();

        //    return customers;
        //}

        //[HttpGet]
        //public async Task<IActionResult> Get()
        //{
        //    var customers = customerService.Get();

        //    return Ok(customers);
        //}


        //[HttpGet("{id}")]
        //public Customer Get(int id)
        //{
        //    var customer = customerService.Get(id);

        //    return customer;
        //}

        [HttpGet("{id:int}")]

        //[HttpGet]
        //[Route("{id:int}")]
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

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] CustomerSearchCriteria criteria)
        {
            var customers = customerService.Get(criteria);

            return Ok(customers);
        }

        [HttpGet]
        public async Task<IActionResult> Get(
            [Required][Range(-90, 90)] double lat,
            [Required][Range(-180, 180)] double lng)
        {
            return Ok();
        }



    }
}
