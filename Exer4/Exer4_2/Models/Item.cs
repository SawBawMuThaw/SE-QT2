using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Exer4_2.Models
{
    public class Item
    {
        [Key]
        public int ItemID { get; set; }
        [MaxLength(50)]
        public string ItemName { get; set; }
        [MaxLength(30)]
        public string? Size { get; set; }
        [DefaultValue(0)]
        [NotNull]
        public int Stock { get; set; }
    }
}
