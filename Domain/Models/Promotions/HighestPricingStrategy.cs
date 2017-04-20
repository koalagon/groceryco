using Domain.Models.Sales;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Promotions
{
    public class HighestPricingStrategy : IPricingStrategy
    {
        readonly ICollection<Promotion> availablePromotion = new HashSet<Promotion>();
        readonly static Logger logger = LogManager.GetCurrentClassLogger();

        public void ApplyPromotion(Promotion promotion)
        {
            availablePromotion.Add(promotion);
        }

        public AppliedPromotion GetAppliedPromotion(SaleLineItem saleLineItem)
        {
            AppliedPromotion appliedPromotion = null;
            decimal maxTotal = decimal.MinValue;

            foreach (Promotion promotion in availablePromotion)
            {
                decimal promoSubTotal = promotion.GetSubTotal(saleLineItem);
                decimal regularPrice = saleLineItem.Quantity * saleLineItem.Product.Price;

                if (maxTotal <= promoSubTotal && promoSubTotal < regularPrice)
                {
                    maxTotal = promoSubTotal;
                    decimal discountAmount = regularPrice - maxTotal;
                    appliedPromotion = new AppliedPromotion(promotion, regularPrice, maxTotal, discountAmount);
                }
            }

            if (appliedPromotion != null)
                logger.Info("Applied Highest Pricing Strategy --- promotion: {0}, regular price: {1}, discount amount: {2}",
                    appliedPromotion.GetPromotion().Description,
                    appliedPromotion.GetRegularPrice(),
                    appliedPromotion.GetDiscountAmount());

            return appliedPromotion;
        }
    }
}
