using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTTPClassLib.Models
{
    public class Order
    {
        public Guid OrderId { get; set; }

        public List<OrderItem> Items { get; set; }
    }

    public class OrderItem
    {
        public string Id { get; set; }

        public double Quantity { get; set; }
    }
}
