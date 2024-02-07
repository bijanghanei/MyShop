using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyShop.Core.Contracts;
using MyShop.Core.Models;
using MyShop.Core.ViewModels;

namespace MyShop.Services
{
    public class OrderService : IOrderService
    {
        private IRepository<Order> orderRepository;

        public OrderService(IRepository<Order> orderRepository)
        {
            this.orderRepository = orderRepository;
        }

        public void CreateOrder(Order order, List<BasketItemViewModel> basketItems)
        {
            foreach (var basketItem in basketItems)
            {
                order.OrderItems.Add(
                    new OrderItem()
                    {
                        Price = basketItem.Price,
                        Quantity = basketItem.Quantity,
                        Image = basketItem.Image,
                        ProductName = basketItem.Name,
                        ProductId = basketItem.Id
                    });
            }
            orderRepository.Insert(order);
            orderRepository.Commit();
        }
    }
}
