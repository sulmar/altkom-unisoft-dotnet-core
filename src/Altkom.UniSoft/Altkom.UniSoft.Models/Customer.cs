using System;

namespace Altkom.UniSoft.Models
{

    public class Customer : Base
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName => $"{FirstName}{LastName}";

        public Gender Gender { get; set; }
        public bool IsRemoved { get; set; }

        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
    }
}
