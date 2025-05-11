using BLL;
using DAL;
using System;
using System.Collections.Generic;
using System.EnterpriseServices;
using System.Linq;
using System.Web;
using System.Web.Services.Description;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Exer1
{
	public partial class MainForm : System.Web.UI.Page
	{
        private readonly ItemRepo Itmrepo;
        private readonly OrderRepo Ordrepo;
        private readonly AgentRepo Agtrepo;

        private List<OrderDetail> orderList = new List<OrderDetail>();
        private int orderId;
        private int agentId;

        public MainForm()
        {
            Itmrepo = new ItemRepo();
            Ordrepo = new OrderRepo();
            Agtrepo = new AgentRepo();
        }

		protected void Page_Load(object sender, EventArgs e)
		{
            ErrorLabel.ForeColor = System.Drawing.Color.Red;
		}

        protected void MultiView1_ActiveViewChanged(object sender, EventArgs e)
        {


        }

        protected void ItemBtn_Click(object sender, EventArgs e)
        {
            MultiView1.SetActiveView(ItemView);

            var items = Itmrepo.GetItems();
            ItemGrid.DataSource = items;
            ItemGrid.DataBind();

            hideItemErrorLabel();

            DropDownList1.DataSource = new List<ListItem>
            {
                new ListItem
                {
                    Text = "Nothing",
                    Value = "0",
                    Selected = true
                },
                new ListItem
                {
                    Text = "Most purchased",
                    Value = "1"
                },
                new ListItem
                {
                    Text = "Unit Amount",
                    Value = "2"
                }
            };
            DropDownList1.DataBind();
        }

        protected void AgentBtn_Click(object sender, EventArgs e)
        {
            MultiView1.SetActiveView(AgentView);

            hideAgentErrorLabel();
            var agents = Agtrepo.GetAgents();
            AgentGrid.DataSource = agents;
            AgentGrid.DataBind();
        }

        protected void OrderBtn_Click(object sender, EventArgs e)
        {
            MultiView1.SetActiveView(OrderView);

            hideOrderErrorLabel();
            OrderGrid.DataSource = Ordrepo.GetExtendedOrder();
            OrderGrid.DataBind();
        }

        protected void GetReportBtn_Click(object sender, EventArgs e)
        {
            string querystring = "/ReportForm.aspx?id=" + txtOrdViewOrdId.Text;
            Response.Redirect(querystring);
        }

        protected void AgentGrid_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void CreateOrdBtn_Click(object sender, EventArgs e)
        {
            hideOrderErrorLabel();
            try
            {
                agentId = Convert.ToInt32(txtAgentId.Text);
            }
            catch(Exception)
            {
                // show error message
                showOrderErrorLabel("Agent ID cannot be empty");
                return;
            }

            Ordrepo.CreateOrder(agentId);

            OrderGrid.DataSource = Ordrepo.GetExtendedOrder();
            OrderGrid.DataBind();
        }

        protected void AddItemBtn_Click(object sender, EventArgs e)
        {
            hideOrdDetErrLabel();
            var orderId = Convert.ToInt32(txtOrdDetOrdId.Text);
            var items = Itmrepo.GetItems();
            var itemId = Convert.ToInt32(ItemList.SelectedValue);
            var item = items.First(i => i.ItemID == itemId);

            if(txtItemQuantity.Text != string.Empty && txtUnitAmount.Text != string.Empty)
            {

                var quantity = Convert.ToInt32(txtItemQuantity.Text);
                var unitAmt = Convert.ToInt32(txtUnitAmount.Text);


                if(Itmrepo.sufficientStock(item.ItemID, quantity))
                { 
                    Ordrepo.AddOrderDetails(new List<OrderDetail> { new OrderDetail { ItemID = itemId, OrderID = orderId, Quantity = quantity, UnitAmount = unitAmt } });

                    OrdDetGrid.DataSource = Ordrepo.GetExtendedOrderDetail(orderId);
                    OrdDetGrid.DataBind();
                }
                else
                {
                    showOrdDetErrorLabel("Insufficient Stock");
                }
            }
            else
            {
                showOrdDetErrorLabel("Quantity and Unit Amount can't be empty");
            }
        }

        private void showOrdDetErrorLabel(string msg)
        {
            OrdDetErrLabel.ForeColor = System.Drawing.Color.Red;
            OrdDetErrLabel.Text = msg;
            OrdDetErrLabel.Visible = true;
        }

        private void hideOrdDetErrLabel()
        {
            OrdDetErrLabel.Visible = false;
        }

        protected void RmvItemBtn_Click(object sender, EventArgs e)
        {
            hideOrdDetErrLabel();
            int id;
            int orderId;
            try
            {
                id = Convert.ToInt32(txtOrdDetID.Text);
                orderId = Convert.ToInt32(txtOrdDetOrdId.Text);
            }
            catch (Exception)
            {
                showOrdDetErrorLabel("Please select an Order Detail ID");
                return;
            }

            Ordrepo.RemoveOrderDetail(id);
            

            OrdDetGrid.DataSource = Ordrepo.GetOrderDetails(orderId);
            OrdDetGrid.DataBind();
        }

        protected void GetOrdBtn_Click(object sender, EventArgs e)
        {
            hideOrderErrorLabel();
            try
            {
               orderId = Convert.ToInt32(txtOrdViewOrdId.Text);
            }
            catch (Exception)
            {
                showOrderErrorLabel("Order ID cannot be empty");
                return;
            }


            Label12.Visible = true;
            txtAgentId.Visible = true;
            Label10.Visible = true;
            txtOrdViewOrdId.Visible = true;

            OrderGrid.DataSource = Ordrepo.GetExtendedOrderDetail(orderId);
            OrderGrid.DataBind();
        }

        protected void AllOrdsBtn_Click(object sender, EventArgs e)
        {
            Label12.Visible = true;
            txtAgentId.Visible = true;
            Label10.Visible = true;
            txtOrdViewOrdId.Visible = true;

            var orders = Ordrepo.GetOrders();
            var agents = Agtrepo.GetAgents();
            var data = from order in orders
                       join agent in agents on order.AgentID equals agent.AgentID
                       select new { OrderId = order.OrderID, OrderDate = order.OrderDate, AgentName = agent.AgentName, AgentID = agent.AgentID };
            OrderGrid.DataSource = data;
            OrderGrid.DataBind();
        }


        // agent create button
        protected void Button1_Click(object sender, EventArgs e)
        {
            string name = txtName.Text;
            string address = txtAddr.Text;

            Agtrepo.AddAgent(name, address);

            AgentGrid.DataSource = Agtrepo.GetAgents();
            AgentGrid.DataBind();
        }

        protected void UpdateAgentBtn_Click(object sender, EventArgs e)
        {
            hideAgentErrorLabel();
            int agentId;
            try
            {
                agentId = Convert.ToInt32(txtAgentViewId.Text);
            }
            catch (Exception)
            {
                showAgentErrorLabel("Agent Id field can't be empty");
                return;
            }
            string name = txtName.Text;
            string address = txtAddr.Text;

            Agtrepo.UpdateAgent(agentId, name, address);

            AgentGrid.DataSource = Agtrepo.GetAgents();
            AgentGrid.DataBind();
        }

        protected void DeleteAgentBtn_Click(object sender, EventArgs e)
        {
            hideAgentErrorLabel();
            
            try
            {
                agentId = Convert.ToInt32(txtAgentViewId.Text);
            }
            catch(Exception)
            {
                showAgentErrorLabel("Agent Field can't be empty");
                return;
            }

            Agtrepo.DeleteAgent(agentId);

            AgentGrid.DataSource = Agtrepo.GetAgents();
            AgentGrid.DataBind();
        }

        protected void AddBtn_Click(object sender, EventArgs e)
        {
            string itemName = txtItemName.Text;
            string itemSize = txtItemSize.Text;
            int itemStock = Convert.ToInt32(txtItemStock.Text);

            if(itemName != string.Empty)
            {
                ItemErrorLabel.Visible = false;
                Itmrepo.AddItem(itemName, itemSize, itemStock);

                ItemGrid.DataSource = Itmrepo.GetItems();
                ItemGrid.DataBind();
            }
            else
            {
                showItemErrorLabel("Name can't be empty");
            }
        }

        protected void DelBtn_Click(object sender, EventArgs e)
        {
            hideItemErrorLabel();
            int itemId;
            try
            {
                itemId = Convert.ToInt32(txtItemViewId.Text);
            }
            catch(Exception)
            {
                showItemErrorLabel("Item Id can't be empty");
                return;
            }

            Itmrepo.RemoveItem(itemId);

            ItemGrid.DataSource = Itmrepo.GetItems();
            ItemGrid.DataBind();
        }

        protected void UpdateBtn_Click(object sender, EventArgs e)
        {
            hideItemErrorLabel();
            int itemId;
            if(txtItemViewId.Text != string.Empty)
            {
                itemId = Convert.ToInt32(txtItemViewId.Text);
            }
            else
            {
                showItemErrorLabel("Item ID can't be empty");
                return;
            }

            string itemName = txtItemName.Text;
            string itemSize = txtItemSize.Text;
            int itemStock = Convert.ToInt32(txtItemStock.Text);

            if (itemName != string.Empty && txtItemStock.Text != string.Empty)
            {
                ItemErrorLabel.Visible = false;
                Itmrepo.UpdateItem(itemId, itemName, itemSize, itemStock);

                ItemGrid.DataSource = Itmrepo.GetItems();
                ItemGrid.DataBind();
            }
            else
            {
                showItemErrorLabel("Name and Stock Can't be empty");
            }
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //int value = Convert.ToInt32(DropDownList1.SelectedValue);
            int value = DropDownList1.SelectedIndex;
            if(value == 0)
            {
                ItemGrid.DataSource = Itmrepo.GetItems();
                ItemGrid.DataBind();
            }else if(value == 1)
            {
                ItemGrid.DataSource = Itmrepo.getExtendedItemList();
                ItemGrid.DataBind();
            }else if(value == 2)
            {
                ItemGrid.DataSource = Itmrepo.GetSortedUnitAmountList();
                ItemGrid.DataBind();
            }
        }

        private void showOrderErrorLabel(string message)
        {
            ErrorLabel.Visible = true;
            ErrorLabel.ForeColor = System.Drawing.Color.Red;
            ErrorLabel.Text = message;
        }

        private void hideOrderErrorLabel()
        {
            ErrorLabel.Visible = false;
        }

        private void showAgentErrorLabel(string message)
        {
            AgentErrorLabel.Visible = true;
            AgentErrorLabel.ForeColor = System.Drawing.Color.Red;
            AgentErrorLabel.Text = message;
        }

        private void hideAgentErrorLabel()
        {
            AgentErrorLabel.Visible = false;
        }

        private void showItemErrorLabel(string message)
        {
            ItemErrorLabel.Visible = true;
            ItemErrorLabel.ForeColor = System.Drawing.Color.Red;
            ItemErrorLabel.Text = message;
        }

        private void hideItemErrorLabel()
        {
            ItemErrorLabel.Visible = false;
        }

        protected void ItemList_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void OrdDetBtn_Click(object sender, EventArgs e)
        {
            MultiView1.SetActiveView(OrderDetailView);

            var items = Itmrepo.GetItems();
            ItemList.DataSource = from i in items
                                  select new ListItem()
                                  {
                                      Text = i.ItemName,
                                      Value = i.ItemID.ToString()
                                  };
            ItemList.DataBind();

            hideOrdDetErrLabel();
            OrdDetGrid.DataSource = new List<OrderDetail>();
            OrdDetGrid.DataBind();
        }

        protected void GetOrdDetBtn_Click(object sender, EventArgs e)
        {
            int orderId;
            if(txtOrdDetOrdId.Text != string.Empty)
            {
                orderId = Convert.ToInt32(txtOrdDetOrdId.Text);
            }
            else
            {
                showOrdDetErrorLabel("Order ID cannot be empty");
                return;
            }

            OrdDetGrid.DataSource = Ordrepo.GetExtendedOrderDetail(orderId);
            OrdDetGrid.DataBind();
        }
    }
}