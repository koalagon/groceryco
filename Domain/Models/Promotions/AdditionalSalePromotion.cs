using Domain.Models.Products;
using Domain.Models.Sales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Promotions
{
    public class AdditionalSalePromotion : Promotion
    {
        public int BuyQuantity { get; set; }
        public float LastProductDiscountPercent { get; set; }

        public AdditionalSalePromotion()
        {

        }

        public AdditionalSalePromotion(string id, DateTime startDate, DateTime endDate, string description, int buyQuantity, float lastProductDiscountPercent)
        {
            base.Id = id;
            base.StartDate = startDate;
            base.EndDate = endDate;
            base.Description = description;
            this.BuyQuantity = buyQuantity;
            this.LastProductDiscountPercent = lastProductDiscountPercent;
        }

        public override decimal GetSubTotal(SaleLineItem item)
        {
            return CalculateNoDiscount(item) + CalculateDiscount(item);
        }


        private decimal CalculateNoDiscount(SaleLineItem item)
        {
            return BuyQuantity * item.Product.Price * (item.Quantity / (BuyQuantity + 1)) + item.Product.Price * (item.Quantity % (BuyQuantity + 1));
        }

        private decimal CalculateDiscount(SaleLineItem item)
        {
            return item.Product.Price * (decimal)LastProductDiscountPercent * (item.Quantity / (BuyQuantity + 1));
        }
    }
}
