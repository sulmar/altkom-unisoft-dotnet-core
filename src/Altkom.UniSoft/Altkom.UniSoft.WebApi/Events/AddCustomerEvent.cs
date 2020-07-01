﻿using Altkom.UniSoft.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Altkom.UniSoft.WebApi.Events
{
    // dotnet add package MediatR   
    public class AddCustomerEvent : INotification  // mark interface
    {
        public AddCustomerEvent(Customer customer)
        {
            Customer = customer;
        }

        public Customer Customer { get; private set; }
    }
}
