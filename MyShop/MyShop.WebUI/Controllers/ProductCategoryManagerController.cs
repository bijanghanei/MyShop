using MyShop.Core.Models;
using MyShop.DataAccess.InMemory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyShop.WebUI.Controllers
{
    public class ProductCategoryManagerController : Controller
    {
        InMemoyRepository<ProductCategory> productCategoryRepository;

        public ProductCategoryManagerController()
        {
            productCategoryRepository = new InMemoyRepository<ProductCategory>();
        }
        //create
        public ActionResult Create()
        {
            ProductCategory productCategory = new ProductCategory();
            return View(productCategory);
        }
        [HttpPost]
        public ActionResult Create(ProductCategory productCategory)
        {
            productCategoryRepository.Insert(productCategory);
            productCategoryRepository.Commit();
            return RedirectToAction("Index");
        }
        // GET: ProductCategoryManager
        public ActionResult Index()
        {
            List<ProductCategory> categories = productCategoryRepository.Collection().ToList();
            return View(categories);
        }
        public ActionResult Edit(string Id)
        {
            ProductCategory productCategory = productCategoryRepository.Find(Id);
            if(productCategory == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(productCategory);
            }
        }
        [HttpPost]
        public ActionResult Edit(ProductCategory productCategory)
        {
            ProductCategory category = productCategoryRepository.Find(productCategory.Id);
            if(category == null)
            {
                return HttpNotFound();
            }
            else
            {
                if (ModelState.IsValid)
                {
                    category.Category = productCategory.Category;
                    productCategoryRepository.Commit();
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(productCategory);
                }
            }
            
        }

        public ActionResult Delete(string Id) 
        {
            ProductCategory category = productCategoryRepository.Find(Id);
            if (category == null)
            {
                return HttpNotFound();
            }
            else
            {

                return View(category);
            }
        }
        [HttpPost]
        public ActionResult Delete(ProductCategory pc)
        {
            ProductCategory category = productCategoryRepository.Find(pc.Id);
            if(category == null)
            {
                return HttpNotFound();
            }
            else
            {
                productCategoryRepository.Delete(pc.Id);
                productCategoryRepository.Commit();
                return RedirectToAction("Index");
            }
        }
    }
}