using MyShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.DataAccess.InMemory
{
    public class ProductRepository
    {
        ObjectCache cache = MemoryCache.Default;
        List<Product> products = new List<Product>();
        public ProductRepository() 
        {
            products = (List<Product>) cache["products"];
            if(products == null )
            {
                products = new List<Product>();
            }
        }

        public void Commit()
        {
            cache["products"] = products;
        }
        //Create
        public void Insert(Product p)
        {
            products.Add(p);
        }


        //Read
        public Product Find (string Id)
        {
            Product product = products.Find(p => p.Id == Id);
            if(product != null)
            {
                return product;
            }
            else
            {
                throw new Exception("Product not found !!!");
            }
        }

        //Update 
        public void Update(Product p)
        {
            Product productToUpdate = products.Find(product => product.Id == p.Id);
            if (productToUpdate != null)
            {
                productToUpdate = p;
            }
            else
            {
                throw new Exception("Product not found !!!");
            }
        }

        //Delete
        public void Delete(string Id)
        {
            Product productToDelete = products.Find(product => product.Id == Id);
            if (productToDelete != null)
            {
                products.Remove(productToDelete);
            }
            else
            {
                throw new Exception("Product not found !!!");
            }
        }

        //Get a queryable list of products
        public IQueryable<Product> Collection()
        {
            return products.AsQueryable();
        }
    }
}
