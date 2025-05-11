using BLL;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Exer1.Tests
{
    public class OrderRepoTest
    {
        [Fact]
        public void NextOrdId_Test()
        {
            OrderRepo repo = new OrderRepo();
            Ex1DbContext context = new Ex1DbContext();

            var expected = context.Orders.Select(o => o.OrderID).Max() + 1;
            var actual = repo.NextOrderId();
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetExtendedOrder_Test()
        {
            OrderRepo repo = new OrderRepo();

            var list = repo.GetExtendedOrder();
            Assert.NotNull(list);
            Assert.IsType<List<ExtOrd>>(list);
        }

        [Fact]
        public void Order_Test()
        {
            var agentId = 1;
            OrderRepo repo = new OrderRepo();
            var orderId = repo.NextOrderId();
            Assert.True(repo.CreateOrder(agentId));

            var createdOrder = repo.GetOrder(orderId);
            Assert.NotNull(createdOrder);
            Assert.Equal(agentId, createdOrder.AgentID);

            List<OrderDetail> testlist = new List<OrderDetail>
            {
                new OrderDetail {OrderID = createdOrder.OrderID, ItemID = 1, Quantity = 15, UnitAmount = 25},
                new OrderDetail {OrderID = createdOrder.OrderID, ItemID = 2, Quantity = 20, UnitAmount = 50}
            };

            Assert.True(repo.AddOrderDetails(testlist));
            Assert.NotEmpty(repo.GetOrderDetails(createdOrder.OrderID));

            var orderDet1 = repo.GetOrderDetails(createdOrder.OrderID)[0];
            var orderDet2 = repo.GetOrderDetails(createdOrder.OrderID)[1];
            Assert.True(repo.RemoveOrderDetail(orderDet1.ID));
            

            var actual = 1;
            Assert.Equal(repo.GetOrderDetails(createdOrder.OrderID).Count, actual);
            //repo.RemoveOrderDetail(orderDet2.ID);

            Assert.True(repo.DeleteOrder(createdOrder.OrderID));
            Assert.Null(repo.GetOrder(createdOrder.OrderID));

        }

    }
}
