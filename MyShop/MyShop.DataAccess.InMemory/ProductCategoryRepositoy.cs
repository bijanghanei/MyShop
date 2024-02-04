using MyShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.DataAccess.InMemory
{
    public class ProductCategoryRepository
    {
        ObjectCache cache = MemoryCache.Default;
        List<ProductCategory> productCategories = new List<ProductCategory>();
        public ProductCategoryRepository()
        {
            productCategories = (List<ProductCategory>)cache["productCategories"];
            if (productCategories == null)
            {
                productCategories = new List<ProductCategory>();
            }
        }

        public void Commit()
        {
            cache["productCategories"] = productCategories;
        }
        //Create
        public void Insert(ProductCategory pc)
        {
            productCategories.Add(pc);
        }


        //Read
        public ProductCategory Find(string Id)
        {
            ProductCategory productCategory = productCategories.Find(pc => pc.Id == Id);
            if (productCategory != null)
            {
                return productCategory;
            }
            else
            {
                throw new Exception("Category not found !!!");
            }
        }

        //Update 
        public void Update(ProductCategory pc)
        {
            ProductCategory productCategoryToUpdate = productCategories.Find(product => product.Id == pc.Id);
            if (productCategoryToUpdate != null)
            {
                productCategoryToUpdate = pc;
            }
            else
            {
                throw new Exception("Category not found !!!");
            }
        }

        //Delete
        public void Delete(string Id)
        {
            ProductCategory productCategoryToDelete = productCategories.Find(productCategory => productCategory.Id == Id);
            if (productCategoryToDelete != null)
            {
                productCategories.Remove(productCategoryToDelete);
            }
            else
            {
                throw new Exception("Category not found !!!");
            }
        }

        //Get a queryable list of product categories
        public IQueryable<ProductCategory> Collection()
        {
            return productCategories.AsQueryable();
        }
    }
}
