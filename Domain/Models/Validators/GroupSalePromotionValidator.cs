using Domain.Exceptions;
using Domain.Models.Promotions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Validators
{
    public class GroupSalePromotionValidator : IPromotionValidator
    {
        private readonly IEnumerable<GroupSalePromotion> groupSalePromotions;

        public GroupSalePromotionValidator(IEnumerable<GroupSalePromotion> groupSalePromotions)
        {
            this.groupSalePromotions = groupSalePromotions;
        }

        public void Validate()
        {
            if (groupSalePromotions.Any(x => x.GroupPrice <= 0))
                throw new PromotionException("Group sale promotion price should be greater than 0.");

            if (groupSalePromotions.Any(x => x.GroupQuantity <= 0))
                throw new PromotionException("Group sale promotion quantity should be greater than 0.");
        }
    }
}
