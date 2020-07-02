using System;

namespace Altkom.UniSoft.Models
{
    public abstract class Item : Base
    {
        public string Name { get; set; }
        public decimal UnitPrice { get; set; }
    }

    public class Product : Item
    {
        public string Color { get; set; }
    }

    public class Service : Item
    {        
        public TimeSpan Period { get; set; }

    }
}
