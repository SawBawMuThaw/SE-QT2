using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    interface IOrderInterface
    {
        bool CreateOrder(int agentId);
        Order GetOrder(int id);
        List<Order> GetOrders();
        bool DeleteOrder(int id);
        List<OrderDetail> GetOrderDetails(int OrderId);
    }
}
