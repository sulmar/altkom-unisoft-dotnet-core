using Altkom.UniSoft.Models;
using Bogus;
using System;
using System.Collections.Generic;
using System.Text;

namespace Altkom.UniSoft.FakeServices.Fakers
{
    public class CustomerFaker : Faker<Customer>
    {
        public CustomerFaker()
        {
            RuleFor(p => p.Id, f => f.IndexFaker);
            RuleFor(p => p.FirstName, f => f.Person.FirstName);
            RuleFor(p => p.LastName, f => f.Person.LastName);
            RuleFor(p => p.Gender, f => (Gender) f.Person.Gender);
            RuleFor(p => p.IsRemoved, f => f.Random.Bool(0.2f));
        }
    }
}
