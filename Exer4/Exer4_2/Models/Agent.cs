using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Exer4_2.Models
{
    public class Agent
    {
        [Key]
        public int AgentID { get; set; }
        [MaxLength(50)]
        public string? AgentName { get; set; }
        [MaxLength(50)]
        public string? Address { get; set; }
    }
}
