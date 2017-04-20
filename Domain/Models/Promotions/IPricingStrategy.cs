using Domain.Models.Sales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Promotions
{
    public interface IPricingStrategy
    {
        void ApplyPromotion(Promotion promotion);
        AppliedPromotion GetAppliedPromotion(SaleLineItem saleLineItem);
    }
}
