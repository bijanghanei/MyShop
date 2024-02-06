using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Core.Models
{
    public class BasketItem : BaseModel
    {
        public string ProductId { get; set; }
        public string BasketId { get; set; }
        public int Quantity { get; set; }
    }
}
