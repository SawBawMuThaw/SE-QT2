using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Exer4_2.Models
{
    public class User
    {
        [Key]
        public int UserID { get; set; }
        [MaxLength(30)]
        public string Username { get; set; }
        [EmailAddress]
        [MaxLength(30)]
        [NotNull]
        public string Email { get; set; }
        [MaxLength(60)]
        [NotNull]
        public string Password { get; set; }
    }
}
