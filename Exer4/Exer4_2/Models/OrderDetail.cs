using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Exer4_2.Models
{
    public class OrderDetail
    {
        [Key]
        public int ID { get; set; }
        [ForeignKey(nameof(Order))]
        [NotNull]
        public int OrderID { get; set; }
        [ForeignKey(nameof(Item))]
        [NotNull]
        public int ItemID { get; set; }
        [DefaultValue(0)]
        [Range(0, int.MaxValue)]
        public int Quantity { get; set; }
        [NotNull]
        public int UnitAmount { get; set; }

        [ValidateNever]
        public Order Order { get; set; }
        [ValidateNever]
        public Item Item { get; set; }
    }
}
