using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
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
