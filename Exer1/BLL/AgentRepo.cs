using BLL.Interfaces;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class AgentRepo : IAgentInterface
    {
        private readonly DALservice service;

        public AgentRepo()
        {
            service = new DALservice();
        }

        public bool AddAgent(string agentName, string address)
        {
            return service.AddAgent(agentName, address);
        }

        public bool DeleteAgent(int id)
        {
            return service.RemoveAgent(id);
        }

        public Agent GetAgentById(int id)
        {
            return service.Agents.FirstOrDefault(a => a.AgentID == id);
        }

        public List<Agent> GetAgents()
        {
            return service.Agents;
        }

        public bool UpdateAgent(int id, string agentName, string address)
        {
            return service.UpdateAgent(id, agentName, address);
        }
    }
}
