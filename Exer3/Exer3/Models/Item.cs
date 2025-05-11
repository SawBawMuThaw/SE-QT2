using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Exer3.Models
{
    [Table("Item")]
    public class Item
    {
        [Key]
        public int ItemID { get; set; }
        public string? ItemName { get; set; }
        public string? Size { get; set; }
        public int Stock { get; set; }
    }
}
