using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Exer3.Models
{
    [Table("Agent")]
    public class Agent
    {
        [Key]
        public int AgentID { get; set; }
        public string AgentName { get; set; }
        public string Address { get; set; }
    }
}
