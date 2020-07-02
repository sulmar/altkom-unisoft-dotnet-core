using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace Altkom.UniSoft.Models
{

    public class Customer : Base
    {
        //[Required]
        //[StringLength(50, MinimumLength = 3)]
        public string FirstName { get; set; }
        //[Required]
        //[StringLength(50)]

        public string SecondName { get; set; }
        public string LastName { get; set; }
        public string FullName => $"{FirstName}{LastName}";

        public Gender Gender { get; set; }
        public bool IsRemoved { get; set; }

        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }

        [EmailAddress]
        public string Email { get; set; }
    }
}
