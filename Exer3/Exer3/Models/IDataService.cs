namespace Exer3.Models
{
    public interface IDataService
    {
        public List<Item> Items { get; set; }
        public List<Order> Orders { get; set; }
        public List<Users> Users { get; set; }
        public List<Agent>  Agents { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }

        public bool AddAgent(string name, string Address);
        public bool UpdateAgent(int id, string name, string address);
        public bool RemoveAgent(int id);
        public bool AddItem(string name, string size, int stock);
        public bool RemoveItem(int id);
        public bool UpdateItem(int id, string name, string size, int stock);
        public bool DeductItems(int id, int quantity);
        public bool AddOrder(int agentId);
        public bool RemoveOrder(int id);
        public bool AddOrderDetails(List<OrderDetail> list);
        public int NextOrderId();
        public bool isEmail(string username);
        public bool autheticateUsername(string username, string password);
        public bool authenticateEmail(string email, string password);
        public bool SufficientStock(int id, int quantity);

    }
}
