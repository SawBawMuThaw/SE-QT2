using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Exer3.Models
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
