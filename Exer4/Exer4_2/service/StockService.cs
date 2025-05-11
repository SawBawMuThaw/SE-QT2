using Exer4_2.Models;

namespace Exer4_2.service
{
    public class StockService : ItemStockInterface
    {
        private readonly Ex4DbContext _context;

        public StockService(Ex4DbContext context)
        {
            _context = context;
        }


        public bool isSuffientStock(int? ItemId, int quantity)
        {
            if(ItemId == null)
            {
                return false;
            }
            var item = _context.Items.First(i => i.ItemID == ItemId);

            if(item.Stock < quantity)
            {
                return false;
            }
            return true;
        }
    }
}
