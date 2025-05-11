namespace Exer3.Models
{
    public class DALservice : IDataService
    {
        private readonly Ex3DbContext context;

        public List<Users> Users { get; set; }
        public List<Item> Items { get; set; }
        public List<Order> Orders { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }
        public List<Agent> Agents { get; set; }

        public DALservice(Ex3DbContext _context)
        {
            context = _context;
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
            if (agent != null)
            {
                agent.AgentName = name;
                agent.Address = address;

                context.Agents.Update(agent);

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
            if (agent != null)
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
            catch (Exception)
            {
                return false;
            }
            Items = context.Items.ToList();
            return false;
        }

        public bool RemoveItem(int id)
        {
            var item = Items.FirstOrDefault(i => i.ItemID == id);

            if (item != null)
            {
                context.Items.Remove(item);

                try
                {
                    context.SaveChanges();
                }
                catch (Exception)
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

            if (item != null)
            {
                context.Items.Update(item);

                try
                {
                    context.SaveChanges();
                }
                catch (Exception)
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
            if (item != null)
            {
                if (quantity <= item.Stock)
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
            if(agentId == 0)
            {
                return false;
            }

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
            return false;
        }

        public bool RemoveOrder(int id)
        {
            var order = Orders.FirstOrDefault(o => o.OrderID == id);
            var deleteDetails = OrderDetails.FindAll(od => od.OrderID == id);

            if (order != null)
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
                }
                catch (Exception)
                {
                    return false;
                }
                Orders = context.Orders.ToList();
                OrderDetails = context.OrderDetails.ToList();
                return false;
            }
            return false;
        }

        public bool AddOrderDetails(List<OrderDetail> list)
        {
            context.OrderDetails.AddRange(list);

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

        public int NextOrderId()
        {
            return Orders.Select(ord => ord.OrderID).Max() + 1;
        }

        public bool isEmail(string username)
        {
            return username.Contains("@");
        }

        public bool autheticateUsername(string username, string password)
        {
            var user = context.Users.FirstOrDefault(u => u.UserName == username);

            if(user != null)
            {
                if(password == user.Password)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }

        public bool authenticateEmail(string email, string password)
        {
            var user = context.Users.FirstOrDefault(u => u.Email == email);

            if(user != null)
            {
                if(password == user.Password)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }

        public bool SufficientStock(int id, int quantity)
        {
            var item = Items.First(i => i.ItemID == id);

            if(quantity > item.Stock)
            {
                return false;
            }
            return true;
        }
    }
}
