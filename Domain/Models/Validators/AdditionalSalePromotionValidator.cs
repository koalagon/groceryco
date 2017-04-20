using Domain.Exceptions;
using Domain.Models.Promotions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Validators
{
    public class AdditionalSalePromotionValidator : IPromotionValidator
    {
        private readonly IEnumerable<AdditionalSalePromotion> additionalSalePromotions;

        public AdditionalSalePromotionValidator(IEnumerable<AdditionalSalePromotion> additionalSalePromotions)
        {
            this.additionalSalePromotions = additionalSalePromotions;
        }

        public void Validate()
        {
            if (additionalSalePromotions.Any(x => x.BuyQuantity <= 0))
                throw new PromotionException("Additional sale promotion quantity should be greater than 0.");

            if (additionalSalePromotions.Any(x => x.LastProductDiscountPercent < 0 || x.LastProductDiscountPercent > 1))
                throw new PromotionException("Additional sale promotion discount percent should be between 0 and 1.0.");
        }
    }
}
