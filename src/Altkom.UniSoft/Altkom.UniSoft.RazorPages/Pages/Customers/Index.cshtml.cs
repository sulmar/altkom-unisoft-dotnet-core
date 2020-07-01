using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Alktom.UniSoft.IServices;
using Altkom.UniSoft.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Altkom.UniSoft.RazorPages.Pages.Customers
{
    public class IndexModel : PageModel
    {
        public string Message { get; set; }
        public ICollection<Customer> Customers { get; set; }

        private readonly ICustomerService customerService;

        public IndexModel(ICustomerService customerService)
        {
            this.customerService = customerService;
        }

        [TempData]
        public int SelectedCustomerId { get; set; }

        public void OnGet()
        {
            Message = "Hello Razor Pages!";

            Customers = customerService.Get();
            
            var id = TempData.Peek("SelectedCustomerId");

            // TempData.Keep()

            //if (TempData["SelectedCustomerId"] != null)
            //{
            //    SelectedCustomerId = (int)TempData["SelectedCustomerId"];
            //}

            // zastąpione przez [TempData]
        }
    }
}