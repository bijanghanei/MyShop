using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyShop.Core.Contracts;
using MyShop.Core.Models;
using MyShop.Core.ViewModels;
using MyShop.WebUI;
using MyShop.WebUI.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace MyShop.WebUI.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void IndexPageDoesReturnProducts()
        {
            //    // Arrange
            IRepository<Product> productRepository = new Mocks.MockRepository<Product>();
            IRepository<ProductCategory> productCategoryRepository = new Mocks.MockRepository<ProductCategory>();
            HomeController controller = new HomeController(productRepository,productCategoryRepository);

            productRepository.Insert(new Product());
            
            //    // Act
            ViewResult result = (ViewResult) controller.Index();
            var viewModel = (ProductListViewModel)result.ViewData.Model;

            //    // Assert
            Assert.AreEqual(1, viewModel.products.Count());
        }

        //[TestMethod]
        //public void About()
        //{
        //    // Arrange
        //    HomeController controller = new HomeController();

        //    // Act
        //    ViewResult result = controller.About() as ViewResult;

        //    // Assert
        //    Assert.AreEqual("Your application description page.", result.ViewBag.Message);
        //}

        //[TestMethod]
        //public void Contact()
        //{
        //    // Arrange
        //    HomeController controller = new HomeController();

        //    // Act
        //    ViewResult result = controller.Contact() as ViewResult;

        //    // Assert
        //    Assert.IsNotNull(result);
        //}
    }
}
