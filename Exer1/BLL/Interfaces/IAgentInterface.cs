using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    interface IAgentInterface
    {
        List<Agent> GetAgents();
        Agent GetAgentById(int id);
        bool AddAgent(string agentName, string address);
        bool UpdateAgent(int id, string agentName, string address);
        bool DeleteAgent(int id);
    }
}
