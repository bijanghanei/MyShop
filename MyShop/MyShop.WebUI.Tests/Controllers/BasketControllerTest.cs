using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyShop.Core.Contracts;
using MyShop.Core.Models;
using MyShop.Core.ViewModels;
using MyShop.Services;
using MyShop.WebUI.Controllers;
using MyShop.WebUI.Tests.Mocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web.Mvc;

namespace MyShop.WebUI.Tests.Controllers
{
    [TestClass]
    public class BasketControllerTest
    {
        [TestMethod]
        public void CanAddBasketItem()
        {
            var httpContext = new MockHttpContext();


            IRepository<Basket> basketRepo = new Mocks.MockRepository<Basket>();
            IRepository<Product> productRepo = new Mocks.MockRepository<Product>();
            IRepository<Order> orderRepo = new Mocks.MockRepository<Order>();
            IRepository<Customer> custmerRepo = new Mocks.MockRepository<Customer>();


            IOrderService orderService = new OrderService(orderRepo);
            IBasketService basketService = new BasketService(basketRepo, productRepo);
            BasketController controller = new BasketController(basketService,orderService, custmerRepo);

            controller.ControllerContext = new System.Web.Mvc.ControllerContext(httpContext,new System.Web.Routing.RouteData(),controller);

            controller.AddToBasket("1");

            Basket basket = basketRepo.Collection().FirstOrDefault();

            Assert.IsNotNull(basket);
            Assert.AreEqual(1, basket.basketItems.Count);
        }
        [TestMethod]
        public void CanGetBasketSumaryViewModel()
        {
            var httpContext = new MockHttpContext();


            IRepository<Basket> basketRepo = new Mocks.MockRepository<Basket>();
            IRepository<Product> productRepo = new Mocks.MockRepository<Product>();
            IRepository<Order> orderRepo = new Mocks.MockRepository<Order>();
            IRepository<Customer> custmerRepo = new Mocks.MockRepository<Customer>();


            IOrderService orderService = new OrderService(orderRepo);

            IBasketService basketService = new BasketService(basketRepo, productRepo);

            BasketController controller = new BasketController(basketService,orderService,custmerRepo);

            controller.ControllerContext = new System.Web.Mvc.ControllerContext(httpContext, new System.Web.Routing.RouteData(), controller);

            productRepo.Insert(new Product()
            {
                Price = 1,
                Id = "1"
            });
            productRepo.Insert(new Product()
            {
                Price = 20,
                Id = "2"
            });
            
            Basket basket = new Basket();

            basket.basketItems.Add(new BasketItem()
            {
                ProductId = "1",
                Quantity = 10,
            });
            basket.basketItems.Add(new BasketItem()
            {
                ProductId = "2",
                Quantity = 1,
            });
            basketRepo.Insert(basket);

            httpContext.Request.Cookies.Add(new System.Web.HttpCookie("eCommerceBasket") { Value = basket.Id });


            var result = (PartialViewResult)controller.BasketSummary();
            var basketSummary = (BasketSummaryViewModel) result.ViewData.Model;

            Assert.AreEqual(11, basketSummary.Count);
            Assert.AreEqual(30, basketSummary.Total);
        }
        [TestMethod]
        public void CanCheckoutAndCreateOrder()
        {
            var httpConetext = new MockHttpContext();

            IRepository<Product> productRepo = new Mocks.MockRepository<Product>();
            IRepository<Basket> basketRepo = new Mocks.MockRepository<Basket>();
            IRepository<Order> orderRepo = new Mocks.MockRepository<Order>();
            IRepository<Customer> custmerRepo = new Mocks.MockRepository<Customer>();


            IOrderService orderService = new OrderService(orderRepo);
            IBasketService basketService = new BasketService(basketRepo,productRepo);

            custmerRepo.Insert(new Customer()
            {
                Id = "1",
                Email = "amoo@gmail.com",
                ZipCode = "123456789",
            });

            IPrincipal fakeUser = new GenericPrincipal(new GenericIdentity("amoo@gmail.com"), null);
            httpConetext.User = fakeUser;

            BasketController controller = new BasketController(basketService,orderService,custmerRepo);

            controller.ControllerContext = new System.Web.Mvc.ControllerContext(httpConetext,new System.Web.Routing.RouteData(),controller);


            // we should have a product Id
            productRepo.Insert(new Product()
            {
                Id = "1",
                Price = 10,
            });
            productRepo.Insert(new Product()
            {
                Id = "2",
                Price = 5,
            });


            // we should have a basket 
            Basket basket = new Basket();

            // use add to basket function to have basket  item in basketItems list
            basket.basketItems.Add(new BasketItem()
            {
                ProductId = "1",
                BasketId = basket.Id,
                Quantity = 1,
            });
            basket.basketItems.Add(new BasketItem()
            {
                ProductId = "2",
                BasketId = basket.Id,
                Quantity = 2,
            });
            basketRepo.Insert(basket);

            httpConetext.Request.Cookies.Add(new System.Web.HttpCookie("eCommerceBasket") { Value = basket.Id });


   
            // use checkout func to get
            Order order = new Order();
            controller.Checkout(order);


            Assert.AreEqual(2,order.OrderItems.Count);
            Assert.AreEqual(0, basket.basketItems.Count);
        }

    }
}
