using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Exer4_2.Models
{
    public class Order
    {
        [Key]
        public int OrderID { get; set; }
        [ValidateNever]
        [NotNull]
        public DateTime OrderDate { get; set; }
        [ForeignKey(nameof(Agent))]
        [NotNull]
        public int AgentID { get; set; }

        [ValidateNever]
        public Agent Agent { get; set; }
        [ValidateNever]
        public List<OrderDetail> OrderDetails { get; set; }
    }
}
