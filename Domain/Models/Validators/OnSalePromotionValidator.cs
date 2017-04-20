using Domain.Exceptions;
using Domain.Models.Promotions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Validators
{
    public class OnSalePromotionValidator : IPromotionValidator
    {
        private readonly IEnumerable<OnSalePromotion> onSalePromotions;

        public OnSalePromotionValidator(IEnumerable<OnSalePromotion> onSalePromotions)
        {
            this.onSalePromotions = onSalePromotions;
        }

        public void Validate()
        {
            if (onSalePromotions.Any(x => x.OnSalePrice <= 0))
                throw new PromotionException("On sale promotion price should be greater than 0.");
        }
    }
}
