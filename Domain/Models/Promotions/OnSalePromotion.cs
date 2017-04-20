using Domain.Models.Products;
using Domain.Models.Sales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Promotions
{
    public class OnSalePromotion : Promotion
    {
        public decimal OnSalePrice { get; set; }

        public OnSalePromotion()
        {

        }

        public OnSalePromotion(string id, DateTime startDate, DateTime endDate, string description, decimal onSalePrice)
        {
            base.Id = id;
            base.StartDate = startDate;
            base.EndDate = endDate;
            base.Description = description;
            this.OnSalePrice = onSalePrice;
        }


        public override decimal GetSubTotal(SaleLineItem item)
        {
            return item.Quantity * OnSalePrice;
        }
    }
}
