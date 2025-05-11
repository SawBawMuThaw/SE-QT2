using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Exer2.Models;
using System.Data.Entity; // Add this for Include

namespace Exer2.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private QT2DbEntities db = new QT2DbEntities();

        public ActionResult Index()
        {
            return View(db.Orders.ToList());
        }

        public ActionResult Create()
        {
            var viewModel = new OrderViewModel
            {
                Order = new Order(),
                OrderDetails = new List<OrderDetail> { new OrderDetail() },
                AvailableItems = db.Items.ToList()
            };
            ViewBag.AgentID = new SelectList(db.Agents, "AgentID", "AgentName");
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(OrderViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                db.Orders.Add(viewModel.Order);
                db.SaveChanges(); // Save Order to get OrderID

                foreach (var detail in viewModel.OrderDetails)
                {
                    detail.OrderID = viewModel.Order.OrderID;
                    db.OrderDetails.Add(detail);
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            viewModel.AvailableItems = db.Items.ToList();
            ViewBag.AgentID = new SelectList(db.Agents, "AgentID", "AgentName", viewModel.Order.AgentID);
            return View(viewModel);
        }

        public ActionResult Print(int id)
        {
            var order = db.Orders
                .Include("OrderDetails.Item") // Use string-based Include for navigation properties
                .Include("Agent")
                .FirstOrDefault(o => o.OrderID == id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }
    }
}