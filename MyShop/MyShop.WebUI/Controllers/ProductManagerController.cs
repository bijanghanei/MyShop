﻿using MyShop.Core.Models;
using MyShop.DataAccess.InMemory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyShop.WebUI.Controllers
{
    public class ProductManagerController : Controller
    {
        ProductRepository productRepository;

        public ProductManagerController() 
        {
            productRepository = new ProductRepository();
        }

        // GET: ProductManagemer
        public ActionResult Index()
        {
            List<Product> products = productRepository.Collection().ToList();
            return View(products);
        }

        //Create product
        public ActionResult Create()
        {
            Product product = new Product();
            return View(product);
        }
        [HttpPost]
        public ActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                productRepository.Insert(product);
                productRepository.Commit();

                return RedirectToAction("Index");
            }
            else
            {
                return View(product);
            }
        }

        public ActionResult Edit(string Id)
        {
            Product product = productRepository.Find(Id);
            if(product != null)
            { 
                return View(product);
            }
            else
            {
                return HttpNotFound();
            }
        }
        [HttpPost]
        public ActionResult Edit(Product product) 
        {
            Product productToEdit = productRepository.Find(product.Id);
            if(productToEdit == null)
            {
                return HttpNotFound();
            }
            else
            {
                if (ModelState.IsValid)
                {
                    productToEdit.Name = product.Name;
                    productToEdit.Description = product.Description;
                    productToEdit.Category = product.Category;
                    productToEdit.Image = product.Image;
                    productToEdit.Price = product.Price;
                    productRepository.Commit();
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(productToEdit);
                }
            }

        }
        public ActionResult Delete(string Id)
        {
            Product product = productRepository.Find(Id);
            if (product != null)
            {
                return View(product);
            }
            else
            {
                return HttpNotFound();
            }
        }
        [HttpPost]
        public ActionResult Delete(Product p)
        {
            productRepository.Delete(p.Id);
            productRepository.Commit();

            return RedirectToAction("Index");
        }

    }
}