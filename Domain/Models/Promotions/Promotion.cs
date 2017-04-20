using Domain.Models.Products;
using Domain.Models.Sales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Promotions
{
    public abstract class Promotion
    {
        // this is product id
        public string Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Description { get; set; }

        public abstract decimal GetSubTotal(SaleLineItem item);
    }
}
