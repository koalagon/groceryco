using Domain.Exceptions;
using Domain.Models.Products;
using Domain.Models.Promotions;
using Domain.Models.Sales;
using Domain.Repositories;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
    public class SaleServiceImpl : ISaleService
    {
        readonly static Logger logger = LogManager.GetCurrentClassLogger();

        readonly IRepository<Product> productRepository;
        readonly IRepository<OnSalePromotion> onSalePromotionRepository;
        readonly IRepository<GroupSalePromotion> groupSalePromotionRepository;
        readonly IRepository<AdditionalSalePromotion> additionalSalePromotionRepository;
        readonly Sale sale = new Sale();

        public SaleServiceImpl(
            IRepository<Product> productRepository, 
            IRepository<OnSalePromotion> onSalePromotionRepository, 
            IRepository<GroupSalePromotion> groupSalePromotionRepository,
            IRepository<AdditionalSalePromotion> additionalSalePromotionRepository)
        {
            this.productRepository = productRepository;
            this.onSalePromotionRepository = onSalePromotionRepository;
            this.groupSalePromotionRepository = groupSalePromotionRepository;
            this.additionalSalePromotionRepository = additionalSalePromotionRepository;
        }

        /// <summary>
        /// Add basket products in sale class
        /// </summary>
        /// <param name="id"></param>
        public void AddProduct(string productId)
        {
            var product = productRepository.GetById(productId);
            sale.AddProduct(product);            
        }



        /// <summary>
        /// Apply promotions and create receipt
        /// </summary>
        /// <returns></returns>
        public Receipt GetReceipt()
        {
            if (onSalePromotionRepository != null)
            {
                foreach (var onSalePromotion in onSalePromotionRepository.Find(x => x.StartDate <= DateTime.Now && x.EndDate >= DateTime.Now))
                    sale.ApplyPromotion(onSalePromotion);
            }

            if (groupSalePromotionRepository != null)
            {
                foreach (var groupSalePromotion in groupSalePromotionRepository.Find(x => x.StartDate <= DateTime.Now && x.EndDate >= DateTime.Now))
                    sale.ApplyPromotion(groupSalePromotion);
            }

            if (additionalSalePromotionRepository != null)
            {
                foreach (var additionalSalePromotion in additionalSalePromotionRepository.Find(x => x.StartDate <= DateTime.Now && x.EndDate >= DateTime.Now))
                    sale.ApplyPromotion(additionalSalePromotion);
            }

            return sale.CreateReceipt();
        }

    }
}
