using MyShop.Core.Contracts;
using MyShop.Core.Models;
using MyShop.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyShop.WebUI.Controllers
{
    public class HomeController : Controller
    {
        IRepository<Product> productRepository;
        IRepository<ProductCategory> productCategoryRepository;

        public HomeController(IRepository<Product> productRepository, IRepository<ProductCategory> productCategoryRepository)
        {
            this.productRepository = productRepository;
            this.productCategoryRepository = productCategoryRepository;
        }

        public ActionResult Index(string category=null)
        {
            List<Product> products;
            List<ProductCategory> categories = productCategoryRepository.Collection().ToList();
            if(category == null)
            {
                products = productRepository.Collection().ToList();
            }
            else
            {
                products = productRepository.Collection().Where(product => product.Category == category).ToList();
            }
            ProductListViewModel model = new ProductListViewModel();
            model.products = products;
            model.categories = categories; 
            return View(model);
        }

        public ActionResult Details(string Id)
        {
            Product product = productRepository.Find(Id);
            if (product == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(product);
            }
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}