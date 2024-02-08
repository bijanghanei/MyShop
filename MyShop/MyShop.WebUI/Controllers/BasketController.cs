using MyShop.Core.Contracts;
using MyShop.Core.Models;
using MyShop.Core.ViewModels;
using MyShop.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyShop.WebUI.Controllers
{
    public class BasketController : Controller
    {
        IBasketService basketService;
        IOrderService orderService;
        IRepository<Customer> customerRepository;
        public BasketController(IBasketService basketService, IOrderService orderService, IRepository<Customer> customerRepository)
        {
            this.basketService = basketService;
            this.orderService = orderService;
            this.customerRepository = customerRepository;
        }
        // GET: Basket
        public ActionResult Index()
        {
            var basketItems = basketService.GetBasketItems(this.HttpContext);
            return View(basketItems);
        }
        public ActionResult AddToBasket(string Id)
        {
            basketService.AddToBasket(this.HttpContext, Id);
            return RedirectToAction("Index");
        }
        public ActionResult RemoveFromBasket(string Id)
        {
            basketService.RemoveFromBasket(this.HttpContext, Id);
            return RedirectToAction("Index");
        }
        public PartialViewResult BasketSummary()
        {
            var summary = basketService.GetBasketSummary(this.HttpContext);
            return PartialView(summary);
        }
        [Authorize]
        public ActionResult Checkout()
        {
            Customer customer = customerRepository.Collection().FirstOrDefault(c => c.Email == User.Identity.Name);
            if (customer == null)
            {
                return RedirectToAction("Error");
            }
            else
            {
                Order order = new Order();
                order.Id = customer.Id;
                order.FirstName = customer.FirstName;
                order.LastName = customer.LastName;
                order.Email = customer.Email;
                order.Street = customer.Street;
                order.City = customer.City;
                order.State = customer.State;
                order.ZipCode = customer.ZipCode;
                return View(order);
            }
        }
        [HttpPost]
        [Authorize]
        public ActionResult Checkout(Order order)
        {
            var basketItems = basketService.GetBasketItems(this.HttpContext);
            orderService.CreateOrder(order, basketItems);
            basketService.ClearBasket(this.HttpContext);
            return RedirectToAction("ThankYou",new {OrderId = order.Id});
        }
        public ActionResult ThankYou(string orderId)
        {
            ViewBag.OrderId = orderId;
            return View();
        }
    }
}