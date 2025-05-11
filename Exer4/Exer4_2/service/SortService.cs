using Exer4_2.Models;

namespace Exer4_2.service
{
    public class SortService : SortInterface
    {
        private readonly Ex4DbContext context;

        public SortService(Ex4DbContext _context)
        {
            context = _context;
        }

        public List<Item> sortBestItems()
        {
            //throw new NotImplementedException();
            var orderdetails = context.OrderDetails.ToList();

            foreach(OrderDetail od in orderdetails)
            {
                od.Item = context.Items.First(i => i.ItemID == od.ItemID);
            }
            var groupByItem = (from od in orderdetails
                              group od by new
                              {
                                  od.Item,
                                  od.Quantity
                              } into itemGp
                              orderby itemGp.Sum(od => od.Quantity) ascending
                              select new Item
                              {
                                  ItemID = itemGp.Key.Item.ItemID,
                                  ItemName = itemGp.Key.Item.ItemName,
                                  Size = itemGp.Key.Item.Size,
                                  Stock = itemGp.Key.Item.Stock
                              }).ToList();


            return groupByItem;
        }

        public List<OrderDetail> sortBoughtItems(int AgentId)
        {
            var orderdetails = context.OrderDetails.ToList();
            foreach(OrderDetail od in orderdetails)
            {
                od.Item = context.Items.First(i => i.ItemID == od.ItemID);
                od.Order = context.Orders.First(o => o.OrderID == od.OrderID);
            }

            return orderdetails.Where(od => od.Order.AgentID == AgentId).ToList();      
        }
    }
}
