using BLL;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;
using Xunit;

namespace Exer1.Tests
{
    public class AgentTests
    {
        private AgentRepo repo { get; set; }

        public AgentTests()
        {
            repo = new AgentRepo();
        }

        [Theory]
        [InlineData("Sheryl Buke","55, Luke Water Lane")]
        public void AddAgent_Test(string name, string address)
        {
            Assert.True(repo.AddAgent(name, address));

            var addedAgent = repo.GetAgents().FirstOrDefault(a => a.AgentName == name);

            Assert.NotNull(addedAgent);
            Assert.Equal(name, addedAgent.AgentName);
            Assert.Equal(address, addedAgent.Address);
        }

        [Theory]
        [InlineData("Sherry Buke", "55, Luke Water Lane", 1)]
        public void UpdateAgent_Test(string name, string address, int agentId)
        {
            Assert.True(repo.UpdateAgent(agentId, name, address));

            var updatedAgent = repo.GetAgentById(agentId);

            Assert.NotNull(updatedAgent);
            Assert.IsType<Agent>(updatedAgent);
            Assert.Equal(name, updatedAgent.AgentName);
            Assert.Equal(address, updatedAgent.Address);

            repo.UpdateAgent(agentId, "Alice Wonderland", "123 Rabbit Hole, Dreamland");
        }

        [Fact]
        public void DeleteAgent_Test()
        {
            int agentId = 19; // change to id of sheryl buke
            Assert.True(repo.DeleteAgent(agentId));

            Assert.Null(repo.GetAgents().FirstOrDefault(a => a.AgentID == agentId));
        }


    }
}
