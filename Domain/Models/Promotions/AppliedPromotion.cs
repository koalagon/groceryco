using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Promotions
{
    public class AppliedPromotion
    {
        private readonly Promotion promotion;
        private readonly decimal regularPrice, discountPrice, discountAmount;

        public AppliedPromotion(Promotion promotion, decimal regularPrice, decimal discountPrice, decimal discountAmount)
        {
            this.promotion = promotion;
            this.regularPrice = regularPrice;
            this.discountPrice = discountPrice;
            this.discountAmount = discountAmount;
        }

        public Promotion GetPromotion()
        {
            return promotion;
        }

        public decimal GetRegularPrice()
        {
            return Math.Round(this.regularPrice, 2);
        }

        public decimal GetDiscountPrice()
        {
            return Math.Round(this.discountPrice, 2);
        }

        public decimal GetDiscountAmount()
        {
            return Math.Round(this.discountAmount, 2);
        }
    }
}
