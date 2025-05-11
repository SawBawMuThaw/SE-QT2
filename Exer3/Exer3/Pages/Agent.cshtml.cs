using Exer3.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Exer3.Pages
{
    public class AgentModel : PageModel
    {
        [BindProperty]
        public int agentId { get; set; }
        [BindProperty]
        public string? agentName { get; set; }
        [BindProperty]
        public string? Address { get; set; }

        public bool showBoughtItems { get; set; } = false;

        private IDataService service { get; set; }

        public List<SelectListItem> agentSelectList { get; set; }
        public List<Agent> agentList { get; set; }
        public List<BoughtItems> boughtItemsList { get; set; }

        public AgentModel(IDataService _service)
        {
            service = _service;
            UpdatePage();
        }

        public void OnGet()
        {
        }

        private void UpdatePage()
        {
            agentSelectList = service.Agents.Select(a => new SelectListItem { Text = a.AgentName, Value = a.AgentID.ToString()}).ToList();
            agentList = service.Agents;
        }


        public void OnPostAddAgent()
        {
            service.AddAgent(agentName, Address);
            UpdatePage();
        }

        public void OnPostUpdateAgent()
        {
            var existing = service.Agents.First(a => a.AgentID == agentId);

            string name, address;

            if(agentName == string.Empty)
            {
                name = existing.AgentName;
            }
            else
            {
                name = agentName;
            }

            if(Address == string.Empty)
            {
                address = existing.Address;
            }
            else
            {
                address = Address;
            }

            service.UpdateAgent(agentId, name, address);
            UpdatePage();
        }

        public void OnPostDeleteAgent(int id)
        {
            service.RemoveAgent(id);
            UpdatePage();
        }

        public void OnPostShowItems()
        {
            boughtItemsList = (from agent in service.Agents
                               join order in service.Orders on agent.AgentID equals order.AgentID
                               join det in service.OrderDetails on order.OrderID equals det.OrderID
                               join item in service.Items on det.ItemID equals item.ItemID
                               where agent.AgentID == agentId
                               select new BoughtItems
                               {
                                   agentId = agent.AgentID,
                                   orderId = order.OrderID,
                                   orderDate = order.OrderDate,
                                   itemName = item.ItemName,
                                   itemSize = item.Size,
                                   quantity = det.Quantity,
                                   unitAmount = det.UnitAmount
                               }).ToList();
            showBoughtItems = true;
        }

        public void OnPostShowAgents()
        {
            showBoughtItems = false;
        }
    }

    public class BoughtItems
    {
        public int agentId { get; set; }
        public int orderId { get; set; }
        public DateTime orderDate { get; set; }
        public string? itemName { get; set; }
        public string? itemSize { get; set; }
        public int quantity { get; set; }
        public int unitAmount { get; set; }
    }
}
