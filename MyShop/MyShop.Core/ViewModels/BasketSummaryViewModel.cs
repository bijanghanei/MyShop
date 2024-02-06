using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Core.ViewModels
{
    public class BasketSummaryViewModel
    {
        public decimal Count { get; set; }
        public decimal Total { get; set; }
        public BasketSummaryViewModel() { }
        public BasketSummaryViewModel(decimal count=0, decimal total = 0)
        {
            Count = count;
            Total = total;
        }
    }
}
