using MyShop.Core.Contracts;
using MyShop.Core.Models;
using MyShop.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MyShop.Services
{
    public class BasketService : IBasketService
    {
        IRepository<Basket> basketRepository;
        IRepository<Product> productRepository;

        public const string basketSessionName = "eCommerceBasket";
        public BasketService(IRepository<Basket> basketRepository, IRepository<Product> productRepository)
        {
            this.basketRepository = basketRepository;
            this.productRepository = productRepository;
        }

        private Basket GetBasket(HttpContextBase httpContext, bool createIfNull)
        {
            Basket basket = new Basket();
            HttpCookie cookie = httpContext.Request.Cookies.Get(basketSessionName);
            if (cookie != null)
            {
                string basketId = cookie.Value;
                if (!string.IsNullOrEmpty(basketId))
                {
                    basket = basketRepository.Find(basketId);
                }
                else
                {
                    if (createIfNull)
                    {
                        basket = CreateBasket(httpContext);
                    }
                }
            }
            else
            {
                if (createIfNull)
                {
                    basket = CreateBasket(httpContext);
                }
            }
            return basket;
        }

        private Basket CreateBasket(HttpContextBase httpContext)
        {
            Basket basket = new Basket();
            basketRepository.Insert(basket);
            basketRepository.Commit();

            HttpCookie cookie = new HttpCookie(basketSessionName);
            cookie.Value = basket.Id;
            cookie.Expires = DateTime.Now.AddDays(1);
            httpContext.Response.Cookies.Add(cookie);

            return basket;
        }
        public void AddToBasket(HttpContextBase httpContext, string productId)
        {
            Basket basket = GetBasket(httpContext, true);
            BasketItem item = basket.basketItems.FirstOrDefault(p => p.ProductId == productId);
            if (item != null)
            {
                item.Quantity++;
            }
            else
            {
                item = new BasketItem()
                {
                    ProductId = productId,
                    BasketId = basket.Id,
                    Quantity = 1,
                };
                basket.basketItems.Add(item);
            }
            basketRepository.Commit();
        }
        public void RemoveFromBasket(HttpContextBase httpContext, string basketItemId)
        {
            Basket basket = GetBasket(httpContext, true);
            BasketItem item = basket.basketItems.FirstOrDefault(p => p.Id == basketItemId);
            if (item != null)
            {
                basket.basketItems.Remove(item);
                basketRepository.Commit();
            }

        }
        public List<BasketItemViewModel> GetBasketItems(HttpContextBase httpContext)
        {
            Basket basket = GetBasket(httpContext, false);
            if (basket != null)
            {
                var basketItemViewModels = (from b in basket.basketItems
                                            join p in productRepository.Collection()
                                            on b.ProductId equals p.Id
                                            select new BasketItemViewModel()
                                            {
                                                Quantity = b.Quantity,
                                                Id = b.Id,
                                                Price = p.Price,
                                                Image = p.Image,
                                                Name = p.Name,

                                             }).ToList();
                return basketItemViewModels;
            }
            else
            {
                return new List<BasketItemViewModel>();
            }
        }
        public BasketSummaryViewModel GetBasketSummary(HttpContextBase httpContext)
        {
            Basket basket = GetBasket(httpContext, false);
            BasketSummaryViewModel summary = new BasketSummaryViewModel();

            if (basket != null)
            {
                decimal? bCount = (from b in basket.basketItems
                                   select b.Quantity).Sum();

                decimal? bTotal = (from b in basket.basketItems
                                   join p in productRepository.Collection()
                                   on b.ProductId equals p.Id
                                   select p.Price * b.Quantity).Sum();
                summary.Total = bTotal ?? 0;
                summary.Count = bCount ?? 0;

                return summary;
            }
            else
            {
                return summary;
            }
        }
    }
}
