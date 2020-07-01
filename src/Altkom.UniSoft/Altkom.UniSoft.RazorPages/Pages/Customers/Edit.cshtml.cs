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
    public class EditModel : PageModel
    {
        private readonly ICustomerService customerService;

        public EditModel(ICustomerService customerService)
        {
            this.customerService = customerService;
        }

        [BindProperty]
        public Customer Customer { get; set; }

        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }

        public IActionResult OnGet()
        {
            Customer = customerService.Get(Id);

            if (Customer == null)
            {
                return RedirectToPage("/NotFound");
            }

            return Page();
        }

        //public IActionResult OnPost(Customer customer)
        //{
        //    this.Customer = customer;

        //    customerService.Update(Customer);

        //    return RedirectToPage("./Index");
        //}

        public IActionResult OnPost()
        {
            customerService.Update(Customer);

            TempData["SelectedCustomerId"] = Customer.Id;

            return RedirectToPage("./Index");

            // return RedirectToPage("./Index", new { SelectedCustomerId = Customer.Id } );
        }
    }
}