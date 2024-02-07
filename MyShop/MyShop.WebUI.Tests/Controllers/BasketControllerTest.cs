using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyShop.Core.Contracts;
using MyShop.Core.Models;
using MyShop.Core.ViewModels;
using MyShop.Services;
using MyShop.WebUI.Controllers;
using MyShop.WebUI.Tests.Mocks;
using System;
using System.Linq;
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

            IBasketService basketService = new BasketService(basketRepo, productRepo);

            BasketController controller = new BasketController(basketService);

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

            IBasketService basketService = new BasketService(basketRepo, productRepo);

            BasketController controller = new BasketController(basketService);

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

    }
}
