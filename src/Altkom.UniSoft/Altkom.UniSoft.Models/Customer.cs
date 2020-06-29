using System;

namespace Altkom.UniSoft.Models
{

    public class Customer : Base
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Gender Gender { get; set; }
        public bool IsRemoved { get; set; }
    }
}
