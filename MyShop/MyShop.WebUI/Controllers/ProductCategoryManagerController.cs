using MyShop.Core.Contracts;
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
        IRepository<ProductCategory> categoryRepository;

        public ProductCategoryManagerController(IRepository<ProductCategory> categoryRepository)
        {
            this.categoryRepository = categoryRepository;
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
            categoryRepository.Insert(productCategory);
            categoryRepository.Commit();
            return RedirectToAction("Index");
        }
        // GET: ProductCategoryManager
        public ActionResult Index()
        {
            List<ProductCategory> categories = categoryRepository.Collection().ToList();
            return View(categories);
        }
        public ActionResult Edit(string Id)
        {
            ProductCategory productCategory = categoryRepository.Find(Id);
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
            ProductCategory category = categoryRepository.Find(productCategory.Id);
            if(category == null)
            {
                return HttpNotFound();
            }
            else
            {
                if (ModelState.IsValid)
                {
                    category.Category = productCategory.Category;
                    categoryRepository.Commit();
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
            ProductCategory category = categoryRepository.Find(Id);
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
            ProductCategory category = categoryRepository.Find(pc.Id);
            if(category == null)
            {
                return HttpNotFound();
            }
            else
            {
                categoryRepository.Delete(pc.Id);
                categoryRepository.Commit();
                return RedirectToAction("Index");
            }
        }
    }
}