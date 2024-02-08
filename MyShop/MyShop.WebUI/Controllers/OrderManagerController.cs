using MyShop.Core.Contracts;
using MyShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyShop.WebUI.Controllers
{
    public class OrderManagerController : Controller
    {
        IOrderService orderService;

        public OrderManagerController(IOrderService orderService)
        {
            this.orderService = orderService;
        }

        // GET: OrderManager
        public ActionResult Index()
        {
            List<Order> orders = orderService.GetAllOrders();
            return View(orders);
        }
        public ActionResult Update(string Id)
        {
            ViewBag.StatusList = new List<string>()
            {
                "Order Created",
                "Payment Proccessed",
                "Order Shipped",
                "Order Completed"
            };
            Order order = orderService.GetOrder(Id);
            return View(order);
        }
        [HttpPost]
        public ActionResult Update(Order updatedOrder,string Id)
        {

            Order order = orderService.GetOrder(Id);
            order.Status = updatedOrder.Status;
            orderService.UpdateOrder(order);
            return RedirectToAction("Index");
        }
    }
}