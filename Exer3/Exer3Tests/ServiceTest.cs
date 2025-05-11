using Exer3.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Exer3Tests
{
    public class ServiceTest
    {
        [Fact]
        public void Agent_Test()
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            string username = "Harriet Tuppet";
            string address = "52, Weir Lane";
            int agentId = 1;

            var options = new DbContextOptionsBuilder<Ex3DbContext>().UseSqlite(connection).Options;

            using(var context = new Ex3DbContext(options))
            {
                context.Database.EnsureCreated();
                var service = new DALservice(context);

                Assert.True(service.AddAgent(username, address));

                var addedAgent = service.Agents.FirstOrDefault(a => a.AgentName == username);

                Assert.NotNull(addedAgent);
                Assert.Equal(username, addedAgent.AgentName);
                Assert.Equal(address, addedAgent.Address);

                Assert.True(service.UpdateAgent(agentId, username, address));

                var updatedAgent = service.Agents.First(a => a.AgentID == agentId);

                Assert.NotNull(updatedAgent);
                Assert.IsType<Agent>(updatedAgent);
                Assert.Equal(username, updatedAgent.AgentName);
                Assert.Equal(address, updatedAgent.Address);

                service.UpdateAgent(agentId, "Alice Wonderland", "123 Rabbit Hole, Dreamland");

                Assert.True(service.RemoveAgent(addedAgent.AgentID));

                Assert.Null(service.Agents.FirstOrDefault(a => a.AgentID == agentId));
            }
        }

        [Fact]
        public void Login_Test()
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

        

            var options = new DbContextOptionsBuilder<Ex3DbContext>().UseSqlite(connection).Options;

            using (var context = new Ex3DbContext(options))
            {
                context.Database.EnsureCreated();
                context.Users.AddRange(
                    new Users { UserID = 1, UserName = "user1", Password = "password123", Email = "user1@example.com" },
                    new Users { UserID = 2, UserName = "testuser", Password = "securepass", Email = "test@domain.net" });
                context.SaveChanges();

                var service = new DALservice(context);

                string email = "user1@example.com";
                string password = "password123";
                bool res = service.authenticateEmail(email, password);
                Assert.True(res);

                string username = "testuser";
                password = "securepass";
                res = service.autheticateUsername(username, password);
                Assert.True(res);

                string input = "user1@example.com";
                res = service.isEmail(input);
                Assert.True(res);

                input = "creator";
                res = service.isEmail(input);
                Assert.False(res);
            }
        }

        [Theory]
        [InlineData("user1", "password123")] // incorrect format
        [InlineData("toget@hotmail.org", "Mesa6&4")] // non-existant user
        [InlineData("user1@example.com", "password333")] // incorrect password
        public void authEmail_fail(string email, string password)
        {

            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            var options = new DbContextOptionsBuilder<Ex3DbContext>().UseSqlite(connection).Options;

            using (var context = new Ex3DbContext(options))
            {
                context.Database.EnsureCreated();
                context.AddRange(
                    new Users { UserID = 1, UserName = "user1", Password = "password123", Email = "user1@example.com" },
                    new Users { UserID = 2, UserName = "testuser", Password = "securepass", Email = "test@domain.net" });

                var service = new DALservice(context);

                bool res = service.authenticateEmail(email, password);
                Assert.False(res);
            }
            
        }

        [Theory]
        [InlineData("sample@email.org", "complex!~@#")]
        [InlineData("toget", "Mesa6&4")]
        [InlineData("testuser", "incorrectPass")]
        public void authUsername_fail(string username, string password)
        {

            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            var options = new DbContextOptionsBuilder<Ex3DbContext>().UseSqlite(connection).Options;

            using (var context = new Ex3DbContext(options))
            {
                context.Database.EnsureCreated();
                context.AddRange(
                    new Users { UserID = 1, UserName = "user1", Password = "password123", Email = "user1@example.com" },
                    new Users { UserID = 2, UserName = "testuser", Password = "securepass", Email = "test@domain.net" });
                context.SaveChanges();

                var service = new DALservice(context);

                bool res = service.autheticateUsername(username, password);
                Assert.False(res);
            }
            
        }

    }
}
