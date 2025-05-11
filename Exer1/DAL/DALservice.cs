using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace DAL
{
    public class DALservice
    {
        private readonly Ex1DbContext context;

        public List<Users> Users { get; set; }
        public List<Item> Items { get; set; }
        public List<Order> Orders { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }
        public List<Agent> Agents { get; set; }

        public DALservice()
        {
            context = new Ex1DbContext();
            Users = context.Users.ToList();
            Items = context.Items.ToList();
            Orders = context.Orders.ToList();
            OrderDetails = context.OrderDetails.ToList();
            Agents = context.Agents.ToList();
        }

        public bool AddAgent(string name, string Address)
        {
            var Agent = new Agent()
            {
                AgentName = name,
                Address = Address
            };
            context.Agents.Add(Agent);

            try
            {
                context.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }
            Agents = context.Agents.ToList();
            return true;
        }
        
        public bool UpdateAgent(int id, string name, string address)
        {
            var agent = Agents.FirstOrDefault(a => a.AgentID == id);
            if(agent != null)
            {
                agent.AgentName = name;
                agent.Address = address;

                context.Agents.AddOrUpdate(agent);

                try
                {
                    context.SaveChanges();
                }
                catch (Exception)
                {
                    return false;
                }
                Agents = context.Agents.ToList();
                return true;
            }
            return false;
        }

        public bool RemoveAgent(int id)
        {
            var agent = Agents.FirstOrDefault(a => a.AgentID == id);
            if(agent != null)
            {
                context.Agents.Remove(agent);
                try
                {
                    context.SaveChanges();
                }
                catch (Exception)
                {
                    return false;
                }
                Agents = context.Agents.ToList();
                return true;
            }
            return false;
        }

        public bool AddItem(string name, string size, int stock)
        {
            var item = new Item()
            {
                ItemName = name,
                Size = size,
                Stock = stock
            };

            context.Items.Add(item);

            try
            {
                context.SaveChanges();
            }
            catch(Exception)
            {
                return false;
            }
            Items = context.Items.ToList();
            return false;
        }

        public bool RemoveItem(int id)
        {
            var item = Items.FirstOrDefault(i => i.ItemID == id);

            if(item != null)
            {
                context.Items.Remove(item);

                try
                {
                    context.SaveChanges();
                }catch(Exception)
                {
                    return false;
                }

                Items = context.Items.ToList();
                return true;
            }
            return false;
        }

        public bool UpdateItem(int id, string name, string size, int stock)
        {
            var item = Items.FirstOrDefault(i => i.ItemID == id);

            if(item != null)
            {
                context.Items.AddOrUpdate(item);

                try
                {
                    context.SaveChanges();
                }
                catch(Exception)
                {
                    return false;
                }
                Items = context.Items.ToList();
                return true;
            }
            return false;
        }

        public bool DeductItems(int id, int quantity)
        {
            var item = Items.FirstOrDefault(i => i.ItemID == id);
            if(item != null)
            {
                if(quantity <= item.Stock)
                {
                    int remaining = item.Stock - quantity;
                    return UpdateItem(id, item.ItemName, item.Size, remaining);
                }
                return false;
            }
            return false;
        }

        // test addorder to make sure it actually returns the id
        public bool AddOrder(int agentId)
        {
            var order = new Order
            {
                OrderDate = DateTime.Today,
                AgentID = agentId
            };

            context.Orders.Add(order);

            try
            {
                context.SaveChanges();
            }
            catch (Exception)
            {

                return false;
            }
            Orders = context.Orders.ToList();
            return true;
        }

        public bool RemoveOrder(int id)
        {
            var order = context.Orders.First(o => o.OrderID == id);
            var deleteDetails = OrderDetails.Where(od => od.OrderID == id);

            if(order != null)
            {
                context.OrderDetails.RemoveRange(deleteDetails);
                try
                {
                    context.SaveChanges();
                }
                catch (Exception)
                {
                    return false;
                }


                context.Orders.Remove(order);
                

                try
                {
                    context.SaveChanges();
                }catch(Exception)
                {
                    return false;
                }
                Orders = context.Orders.ToList();
                OrderDetails = context.OrderDetails.ToList();
                return true;
            }
            return false;
        }

        public bool AddOrderDetails(List<OrderDetail> list)
        {
            context.OrderDetails.AddRange(list);

            try
            {
                context.SaveChanges();
            }catch(Exception)
            {
                return false;
            }
            OrderDetails = context.OrderDetails.ToList();
            return true;
        }

        public int NextOrderId()
        {
            return Orders.Select(ord => ord.OrderID).Max() + 1;
        }

        public bool RemoveOrderDetail(int id)
        {
            var orderDetail = context.OrderDetails.First(od => od.ID == id);

            context.OrderDetails.Remove(orderDetail);

            try
            {
                context.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }

            OrderDetails = context.OrderDetails.ToList();
            return true;
        }
    }
}
