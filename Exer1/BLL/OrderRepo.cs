using BLL.Interfaces;
using DAL;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class OrderRepo : IOrderInterface
    {
        private readonly DALservice service;

        public OrderRepo()
        {
            service = new DALservice();
        }

        public bool CreateOrder(int agentId)
        {
            return service.AddOrder(agentId);
        }

        public bool DeleteOrder(int id)
        {
            return service.RemoveOrder(id);
        }

        public Order GetOrder(int id)
        {
            return service.Orders.FirstOrDefault(o => o.OrderID == id);
        }

        public List<OrderDetail> GetOrderDetails(int OrderId)
        {
            return service.OrderDetails.FindAll(od => od.OrderID == OrderId).ToList();
        }

        public List<Order> GetOrders()
        {
            return service.Orders;
        }

        public bool AddOrderDetails(List<OrderDetail> list)
        {
            return service.AddOrderDetails(list);
        }

        public int NextOrderId()
        {
            return service.NextOrderId();
        }

        public List<ExtOrd> GetExtendedOrder()
        {
            var orders = service.Orders;
            var agents = service.Agents;

            return (from order in orders
                   join agent in agents on order.AgentID equals agent.AgentID
                   select new ExtOrd { OrderId = order.OrderID, OrderDate = order.OrderDate, AgentName = agent.AgentName, AgentID = agent.AgentID }).ToList();
        }

        public List<ExtOrdDet> GetExtendedOrderDetail(int orderId)
        {
            var orderDetails = service.OrderDetails.Where(od => od.OrderID == orderId);
            var items = service.Items;
            return (from orddet in orderDetails
                   join item in items on orddet.ItemID equals item.ItemID
                   select new ExtOrdDet { ID = orddet.ID, OrderID = orddet.OrderID, ItemName = item.ItemName, ItemId = orddet.ItemID, Quantity = orddet.Quantity, UnitAmount = orddet.UnitAmount }).ToList();
            
        }

        public bool RemoveOrderDetail(int id)
        {
            return service.RemoveOrderDetail(id);
        }
    }

    public class ExtOrd
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public string AgentName { get; set; }
        public int AgentID { get; set; }
    }

    public class ExtOrdDet
    {
        public int ID { get; set; }
        public int OrderID { get; set; }
        public string ItemName { get; set; }
        public int ItemId { get; set; }
        public int Quantity { get; set; }
        public int UnitAmount { get; set; }
    }
}
