using Exer3.Models;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exer3Tests
{
    public class OrderTest
    {
        [Fact]
        public void orderTest_pass()
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            var options = new DbContextOptionsBuilder<Ex3DbContext>().UseSqlite(connection).Options;

            using (var context = new Ex3DbContext(options))
            {
                context.Database.EnsureCreated();
                context.Agents.AddRange(
                    new Agent { AgentID = 1, AgentName = "Smith", Address = "123, Wier Lane"}
                );
                context.SaveChanges();

                var service = new DALservice(context);

                var agentId = 1;
                service.AddOrder(agentId);

                var order = service.Orders.FirstOrDefault(o => o.AgentID == agentId);
                Assert.NotNull(order);

                service.RemoveOrder(order.OrderID);
                Assert.Null(service.Orders.FirstOrDefault(o => o.AgentID == order.OrderID));
            }
        }
    }
}
