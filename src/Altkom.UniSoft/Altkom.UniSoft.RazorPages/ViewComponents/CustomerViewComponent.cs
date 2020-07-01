using Altkom.UniSoft.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Altkom.UniSoft.RazorPages.ViewComponents
{
    public class CustomerViewComponent: ViewComponent
    {
        public IViewComponentResult Invoke(Customer customer)
        {
            return View(customer);
        }
    }
}
