using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Altkom.UniSoft.Models
{
    public class Order : Base
    {
        public string Number { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime? ShippedDate { get; set; }
        public Customer Customer { get; set; }      // Navigation property
        public OrderStatus Status { get; set; }
        public ICollection<OrderDetail> Details { get; set; }

        public Order()
        {
            Details = new List<OrderDetail>();
        }
    }

    public class OrderDetail : Base
    {
        public Item Item { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }

    public enum OrderStatus
    {
        Draft,
        Ordered,
        Canceled,
        Done
    }
}
