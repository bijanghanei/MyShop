using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Core.Models
{
    public class Basket : BaseModel
    {
        public virtual ICollection<BasketItem> basketItems { get; set; }
        public Basket()
        {
            basketItems = new List<BasketItem>();
        }
    }
}
