using Domain.Models.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Sales
{
    public class SaleLineItem
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }


        public decimal GetSubTotal()
        {
            return Product.Price * Quantity;
        }
    }
}
