using MyShop.Core.Models;
using MyShop.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Core.Contracts
{
    public interface IOrderService
    {
        void CreateOrder(Order order,List<BasketItemViewModel> basketItems);
        List<Order> GetAllOrders();
        Order GetOrder(string id);
        void UpdateOrder(Order order);
    }
}
