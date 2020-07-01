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
    public class DetailsModel : PageModel
    {
        private readonly ICustomerService customerService;

        public Customer Customer { get; set; }

        public DetailsModel(ICustomerService customerService)
        {
            this.customerService = customerService;
        }

        public IActionResult OnGet(int id, Gender filter, Location location)
        {
            Customer = customerService.Get(id);

            if (Customer == null)
            {
                return RedirectToPage("/NotFound");
            }

            return Page();
        }
    }
}