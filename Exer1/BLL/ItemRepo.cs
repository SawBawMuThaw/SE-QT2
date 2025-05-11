using BLL.Interfaces;
using DAL;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BLL
{
    public class ItemRepo : IItemInterface
    {
        private readonly DALservice service;

        public ItemRepo()
        {
            service = new DALservice();
        }

        public bool AddItem(string itemName, string size, int stock)
        {
            return service.AddItem(itemName, size == string.Empty ? string.Empty : size, stock);
        }

        public Item GetItem(int id)
        {
            return service.Items.FirstOrDefault(i => i.ItemID == id);
        }

        public List<Item> GetItems()
        {
            return service.Items;
        }

        public bool RemoveItem(int id)
        {
            return service.RemoveItem(id);
        }

        public bool UpdateItem(int id, string name, string size, int stock)
        {
            return service.UpdateItem(id, name, size == string.Empty ? string.Empty : size, stock);
        }

        public bool DeductItem(int id, int quantity)
        {
            return service.DeductItems(id, quantity);
        }

        public bool sufficientStock(int id, int quantity)
        {
            var item = service.Items.FirstOrDefault(i => i.ItemID == id);

            if(item != null)
            {
                return quantity <= item.Stock ? true : false;
            }
            return false;
            
        }

        public List<ExtItem> getExtendedItemList()
        {

            return (from item in service.Items
                    join det in service.OrderDetails on item.ItemID equals det.ItemID
                    group det by new
                    {
                        det.ItemID,
                        item.ItemName,
                        det.Quantity
                    } into gcs
                    select new ExtItem
                    {
                        ItemId = gcs.Key.ItemID,
                        ItemName = gcs.Key.ItemName,
                        Quantity = gcs.Sum(d => d.Quantity)
                    }).OrderByDescending(d => d.Quantity).ToList();



        }

        public List<ExtItem2> GetSortedUnitAmountList()
        {
            return (from item in service.Items
                    join det in service.OrderDetails on item.ItemID equals det.ItemID
                    orderby det.UnitAmount descending
                    select new ExtItem2
                    {
                        ItemId = det.ItemID,
                        ItemName = item.ItemName,
                        UnitAmount = det.UnitAmount
                    }).ToList();
        }
    }

    public class ExtItem
    {
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public int Quantity { get; set; }
    }

    public class ExtItem2
    {
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public int UnitAmount { get; set; }
    }
}
