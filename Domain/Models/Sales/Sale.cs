using Domain.Models.Products;
using Domain.Models.Promotions;
using NLog;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Sales
{
    public class Sale
    {
        readonly ICollection<SaleLineItem> saleLineItems = new HashSet<SaleLineItem>();
        readonly ICollection<Promotion> availablePromotions = new HashSet<Promotion>();

        /// <summary>
        /// Add promotion
        /// </summary>
        /// <param name="promotion"></param>
        public void ApplyPromotion(Promotion promotion)
        {
            if (promotion.StartDate <= DateTime.Now && promotion.EndDate >= DateTime.Now)
                availablePromotions.Add(promotion);
        }




        /// <summary>
        /// Clear all promotions
        /// </summary>
        public void ClearPromotions()
        {
            availablePromotions.Clear();
        }




        /// <summary>
        /// Add product
        /// </summary>
        /// <param name="product"></param>
        public void AddProduct(Product product)
        {
            var saleLineItem = GetSaleLineItem(product);
            if (saleLineItem != null)
            {
                saleLineItem.Quantity++;
            }
            else
            {
                saleLineItems.Add(new SaleLineItem { Product = product, Quantity = 1 });
            }
        }


        /// <summary>
        /// Return SaleLineItem having the product
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public SaleLineItem GetSaleLineItem(Product product)
        {
            return saleLineItems.Where(x => x.Product.Id == product.Id).FirstOrDefault();
        }

        
        /// <summary>
        /// Calculate all price information and return receipt class
        /// </summary>
        /// <returns></returns>
        public Receipt CreateReceipt()
        {
            var promotionResults = CalculatePromotions();
            Receipt receipt = new Receipt(saleLineItems, promotionResults);

            return receipt;
        }


        /// <summary>
        /// Return all sale line items
        /// </summary>
        /// <returns></returns>
        public ICollection<SaleLineItem> GetSaleLineItems()
        {
            return saleLineItems;
        }



        /// <summary>
        /// Get best offer promotions
        /// </summary>
        /// <returns></returns>
        protected ICollection<AppliedPromotion> CalculatePromotions()
        {
            ICollection<AppliedPromotion> appliedPromotions = new HashSet<AppliedPromotion>();

            foreach (var saleLineItem in saleLineItems)
            {
                IPricingStrategy pricingStrategy = PricingStrategyFactory.NewInstance().CreateStrategy();            
                IEnumerable<Promotion> promotions = availablePromotions.Where(x => x.Id == saleLineItem.Product.Id);
                foreach (Promotion promotion in promotions)
                    pricingStrategy.ApplyPromotion(promotion);

                var appliedPromotion = pricingStrategy.GetAppliedPromotion(saleLineItem);
                if (appliedPromotion != null)
                    appliedPromotions.Add(appliedPromotion);
            }

            return appliedPromotions;
        }


    }
}
