using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    interface IItemInterface
    {
        bool AddItem(string itemName, string size, int stock);
        List<Item> GetItems();
        Item GetItem(int id);
        bool RemoveItem(int id);
        bool UpdateItem(int id, string name, string size, int stock);
    }
}
