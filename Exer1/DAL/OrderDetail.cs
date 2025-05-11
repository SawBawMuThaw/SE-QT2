using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    [Table("OrderDetail")]
    public class OrderDetail
    {
        [Key]
        public int ID { get; set; }
        //[ForeignKey(nameof(OrderID))]
        public int OrderID { get; set; }
        //[ForeignKey(nameof(ItemID))]
        public int ItemID { get; set; }
        public int Quantity { get; set; }
        public int UnitAmount { get; set; }
    }
}
