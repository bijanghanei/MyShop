using MyShop.Core.Contracts;
using MyShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.WebUI.Tests.Mocks
{
    public class MockRepository<T> : IRepository<T> where T : BaseModel
    {
        List<T> items;
        string className;

        public MockRepository()
        {
            className = typeof(T).Name;
            if (items == null)
            {
                items = new List<T>();
            }
        }

        public void Commit()
        {
        }

        public void Insert(T item)
        {
            items.Add(item);
        }
        public T Find(string Id)
        {
            T item = items.Find(i => i.Id == Id);
            if (item == null)
            {
                throw new Exception($"{className} not found");
            }
            else
            {
                return item;
            }
        }
        public void Update(T item)
        {
            T itemtoUpdate = items.Find(i => i.Id == item.Id);
            if (itemtoUpdate == null)
            {
                throw new Exception($"{className} not found");
            }
            else
            {
                itemtoUpdate = item;
            }
        }

        public void Delete(string Id)
        {
            T itemtoDelete = items.Find(i => i.Id == Id);
            if (itemtoDelete == null)
            {
                throw new Exception($"{className} not found");
            }
            else
            {
                items.Remove(itemtoDelete);
            }
        }

        public IQueryable<T> Collection()
        {
            return items.AsQueryable();
        }
    }
}
