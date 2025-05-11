using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Exer3.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Exer3.Pages
{
    public class OrderDetailModel : PageModel
    {
        [BindProperty]
        public int itemId { get; set; }
        [BindProperty]
        public int quantity { get; set; } = -1;
        [BindProperty]
        public int unitAmount { get; set; } = -1;
        [BindProperty]
        public int orderId { get; set; }
        [BindProperty]
        public int agentId { get; set; }

        public List<OrderDetail> detailList { get; set; } = new List<OrderDetail>();

        public List<SelectListItem> itemList { get; set; }

        private IDataService service { get; set; }
        private Ex3DbContext context { get; set; }
                        

        public OrderDetailModel(IDataService _service, Ex3DbContext _context)
        {
            service = _service;
            context = _context;
            itemList = (service.Items.Select(i => new SelectListItem { Text = i.ItemName, Value = i.ItemID.ToString() })).ToList();
            UpdatePage();
        }

        public void OnGet(int orderId, int agentId)
        {
            this.orderId = orderId;
            service.AddOrder(agentId);
            this.agentId = agentId;
        }

        private void UpdatePage()
        {
            //var orderId = context.OrderDetails.Max(d => d.OrderID);
            detailList = context.OrderDetails.Where(d => d.OrderID == orderId).ToList();
        }

        public void OnPostAddItem()
        {
            //if (service.SufficientStock(itemId, quantity))
            //{
            //    if (detailList.Exists(d => d.ItemID == itemId))
            //    {
            //        var existing = detailList.First(d => d.ItemID == itemId);
            //        existing.Quantity += quantity;
            //        service.DeductItems(itemId, quantity);
            //        UpdatePage();
            //    }
            //    else
            //    {
            //        if (quantity < 0 || unitAmount < 0)
            //        {
            //            ModelState.AddModelError("Undefined field Error", "Quantity and Unit Amount are required fields");
            //            return;
            //        }
            //        else
            //        {
            //            detailList.Add(new OrderDetail
            //            {
            //                OrderID = service.NextOrderId(),
            //                ItemID = itemId,
            //                Quantity = quantity,
            //                UnitAmount = unitAmount
            //            });

            //            service.DeductItems(itemId, quantity);
            //            UpdatePage();
            //        }
            //    }
            //}
            //else
            //{
            //    ModelState.AddModelError("Insufficient Stock", "There is not enough stock of this item");
            //}

            var item = context.Items.First(i => i.ItemID == itemId);

            if(quantity < 0 && unitAmount < 0)
            {
                ModelState.AddModelError("Invalid Values", "Quantity and UnitAmount are required fields");
                return;
            }
            
            if(quantity <= item.Stock)
            {
                item.Stock -= quantity;
                context.Items.Update(item);

                var existing = context.OrderDetails.FirstOrDefault(d => d.ItemID == itemId && d.OrderID == orderId);

                if(existing != null)
                {
                    existing.Quantity += quantity;
                    existing.UnitAmount = unitAmount;
                    context.OrderDetails.Update(existing);
                }
                else
                {
                    var newdet = new OrderDetail
                    {
                        OrderID = orderId,
                        ItemID = itemId,
                        Quantity = quantity,
                        UnitAmount = unitAmount
                    };
                    context.OrderDetails.Add(newdet);
                }
                context.SaveChanges();
                UpdatePage();
            }
            else
            {
                ModelState.AddModelError("Insufficient Stock", "There is not enough stock of this item");
            }
        }

        public void OnPostDeleteItem()
        {
            //var item = service.Items.First(i => i.ItemID == id);

            //var det = detailList.First(d => d.ItemID == id);

            //// to readd all the stuff
            //service.UpdateItem(id, item.ItemName, item.Size, item.Stock + det.Quantity);

            //detailList.Remove(det);
            //UpdatePage();

            var orderId = context.OrderDetails.Max(d => d.OrderID);
            var existing = context.OrderDetails.First(d => d.ItemID == itemId && d.OrderID == orderId);

            var item = context.Items.First(i => i.ItemID == existing.ItemID);
            item.Stock += existing.Quantity;
            context.Items.Update(item);

            context.OrderDetails.Remove(existing);

            context.SaveChanges();
            UpdatePage();
        }

    }
}
