using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Exer2.Models
{
    public class OrderViewModel
    {
        public Order Order { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }
        public List<Item> AvailableItems { get; set; }
    }
}