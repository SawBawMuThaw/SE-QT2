using Exer3.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Exer3.Pages
{
    public class OrderModel : PageModel
    {
        [BindProperty]
        public int agentId { get; set; }
        [BindProperty]
        public int itemId { get; set; }
        [BindProperty]
        public int quantity { get; set; } = -1;
        [BindProperty]
        public int unitAmount { get; set; } = -1;
        [BindProperty]
        public int orderId { get; set; }

        public List<SelectListItem> agentList { get; set; }
        public List<SelectListItem> itemList { get; set; }
        public List<SelectListItem> orderList { get; set; }
        public List<OrderDetail> detailList { get; set; }
        public List<OrderDetail> orderReportList { get; set; }
        public List<Order> ordersList { get; set; }

        public bool showReportMode { get; set; } = false;
        public int tempMaxOrdId { get; set; }

        private IDataService service { get; set; }

        public OrderModel(IDataService _service)
        {
            service = _service;
            detailList = new List<OrderDetail>();
            UpdatePage();
        }

        private void UpdatePage()
        {
            agentList = (service.Agents.Select(a => new SelectListItem { Text = a.AgentName, Value = a.AgentID.ToString() })).ToList();
            itemList = (service.Items.Select(i => new SelectListItem { Text = i.ItemName, Value = i.ItemID.ToString() })).ToList();
            orderList = (service.Orders.Select(o => new SelectListItem { Text = o.OrderID.ToString(), Value = o.OrderID.ToString() })).ToList();
            ordersList = service.Orders;
        }

        public void OnGet()
        {
        }

        public void OnPostGetReport()
        {
            showReportMode = true;
            orderReportList = service.OrderDetails.Where(d => d.OrderID == orderId).ToList();
        }

        public ActionResult OnPostCreateOrd()
        {
            tempMaxOrdId = service.NextOrderId();
            return RedirectToPage("OrderDetail", new { orderId = service.NextOrderId(), agentId = agentId });
        }

        public void OnPostGetOrders()
        {
            showReportMode = false;
        }
    }
}
