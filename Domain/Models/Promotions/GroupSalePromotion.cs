using Domain.Models.Products;
using Domain.Models.Sales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Promotions
{
    public class GroupSalePromotion : Promotion
    {
        public int GroupQuantity { get; set; }
        public decimal GroupPrice { get; set; }

        public GroupSalePromotion()
        {

        }

        public GroupSalePromotion(string id, DateTime startDate, DateTime endDate, string description, int groupQuantity, decimal groupPrice)
        {
            base.Id = id;
            base.StartDate = startDate;
            base.EndDate = endDate;
            base.Description = description;
            this.GroupQuantity = groupQuantity;
            this.GroupPrice = groupPrice;
        }

        public override decimal GetSubTotal(SaleLineItem item)
        {
            if (item.Quantity >= GroupQuantity)
                return (int)(item.Quantity / GroupQuantity) * GroupPrice + (int)(item.Quantity % GroupQuantity) * item.Product.Price;
            else
                return item.Quantity * item.Product.Price;
        }
    }
}
