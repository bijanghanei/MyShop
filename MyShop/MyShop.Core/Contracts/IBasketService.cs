using MyShop.Core.Models;
using MyShop.Core.ViewModels;
using System.Collections.Generic;
using System.Web;

namespace MyShop.Services
{
    public interface IBasketService
    {
        void AddToBasket(HttpContext httpContext, Product product);
        List<BasketItemViewModel> GetBasketItems(HttpContext httpContext);
        BasketSummaryViewModel GetBasketSummary(HttpContext httpContext);
        void RemoveFromBasket(HttpContext httpContext, string basketItemId);
    }
}