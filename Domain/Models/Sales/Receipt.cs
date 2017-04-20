using Domain.Models.Promotions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Sales
{
    public class Receipt
    {
        private readonly ICollection<SaleLineItem> saleLineItems;
        private readonly ICollection<AppliedPromotion> appliedPromotions;

        public Receipt(ICollection<SaleLineItem> saleLineItems, ICollection<AppliedPromotion> appliedPromotions)
        {
            this.saleLineItems = saleLineItems;
            this.appliedPromotions = appliedPromotions;
        }

        public ICollection<SaleLineItem> GetSaleLineItems()
        {
            return saleLineItems;
        }

        public ICollection<AppliedPromotion> GetAppliedPromotions()
        {
            return appliedPromotions;
        }

        public decimal GetTotalRegularPrice()
        {
            return Math.Round(saleLineItems.Sum(x => x.GetSubTotal()), 2);
        }

        public decimal GetTotalSavedMoney()
        {
            return Math.Round(appliedPromotions.Sum(x => x.GetDiscountAmount()), 2);
        }

        public decimal GetTotalPayment()
        {
            return Math.Round(GetTotalRegularPrice() - GetTotalSavedMoney(), 2);
        }

    }
}
