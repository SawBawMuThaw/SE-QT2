using Exer3.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Exer3.Pages
{
    public class ItemModel : PageModel
    {
        [BindProperty]
        public int itemId { get; set; }
        [BindProperty]
        public string? itemName { get; set; }
        [BindProperty]
        public string? itemSize { get; set; }
        [BindProperty]
        public int itemStock { get; set; }

        public List<SelectListItem> options { get; set; }
        public List<Item> itemList { get; set; }

        private IDataService service { get; set; }

        [BindProperty]
        public int sortIndex { get; set; }
        public List<SelectListItem> sortOpts { get; set; }
        public List<ExtItem> sortedList { get; set; }

        public ItemModel(IDataService _service)
        {
            service = _service;
            sortOpts = new List<SelectListItem>
            {
                new SelectListItem { Text = "Nothing" , Value = "0", Selected = true},
                new SelectListItem { Text = "Best Items", Value = "1"}
            };
            UpdatePage();
        }

        public void OnGet()
        {
            UpdatePage();
        }

        public void OnPost()
        {

        }

        private void UpdatePage()
        {
            options = service.Items.Select(i => new SelectListItem
            {
                Text = i.ItemName,
                Value = i.ItemID.ToString()
            }).ToList();
            itemList = service.Items;
            sortedList = (from item in service.Items
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

        public void OnPostAddItem()
        {
           service.AddItem(itemName, itemSize == string.Empty ? "" : itemSize, itemStock);
            UpdatePage();
        }

        public void OnPostUpdateItem()
        {
            var existing = service.Items.First(i => i.ItemID == itemId);

            string name; string? size; int stock;

            if (itemName == string.Empty)
            {
                name = existing.ItemName;
            }
            else
            {
                name = itemName;
            }
            if (itemSize == string.Empty)
            {
                size = existing.Size == string.Empty ? "" : existing.Size;
            }
            else
            {
                size = itemSize;
            }
            if (existing.Stock != itemStock)
            {
                stock = existing.Stock;
            }
            else
            {
                stock = itemStock;
            }

            service.UpdateItem(itemId, name, size, stock);
            UpdatePage();
        }

        public void OnPostDeleteItem(int itemId)
        {
            service.RemoveItem(itemId);
            UpdatePage();
        }
    }
}
