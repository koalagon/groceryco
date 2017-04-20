using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Domain.Repositories;
using Domain.Models.Products;
using Moq;
using System.Collections;
using System.Collections.Generic;
using Domain.Services;
using System.Linq;
using Domain.Models.Promotions;
using Domain.Models.Sales;

namespace UnitTest.ServiceTests
{
    [TestClass]
    public class SaleServiceUnitTest
    {
        IList<Product> products;
        IList<OnSalePromotion> onSalePromotions = new List<OnSalePromotion>();
        IList<GroupSalePromotion> groupSalePromotions = new List<GroupSalePromotion>();
        IList<AdditionalSalePromotion> additionalSalePromotions = new List<AdditionalSalePromotion>();

        Mock<IRepository<Product>> productRepository;
        Mock<IRepository<OnSalePromotion>> onSalePromotionRepository;
        Mock<IRepository<GroupSalePromotion>> groupSalePromotionRepository;
        Mock<IRepository<AdditionalSalePromotion>> additionalSalePromotionRepository;

        [TestInitialize]
        public void TestInit()
        {
            products = new List<Product>()
            {
                new Product() { Id = "Apple", Price = 0.75m },
                new Product() { Id = "Banana", Price = 1.00m },
                new Product() { Id = "Orange", Price = 1.50m },
            };

            

            productRepository = new Mock<IRepository<Product>>();
            productRepository.Setup(x => x.GetAll()).Returns(products);
            productRepository.Setup(x => x.GetById(It.IsAny<string>())).Returns((string id) => products.Single(y => y.Id == id));

            onSalePromotionRepository = new Mock<IRepository<OnSalePromotion>>();
            onSalePromotionRepository.Setup(x => x.Find(It.IsAny<Func<OnSalePromotion, bool>>()))
                .Returns((Func<OnSalePromotion, bool> predicate) => onSalePromotions.Where(predicate));            

            groupSalePromotionRepository = new Mock<IRepository<GroupSalePromotion>>();
            groupSalePromotionRepository.Setup(x => x.Find(It.IsAny<Func<GroupSalePromotion, bool>>()))
                .Returns((Func<GroupSalePromotion, bool> predicate) => groupSalePromotions.Where(predicate));              

            additionalSalePromotionRepository = new Mock<IRepository<AdditionalSalePromotion>>();
            additionalSalePromotionRepository.Setup(x => x.Find(It.IsAny<Func<AdditionalSalePromotion, bool>>()))
                .Returns((Func<AdditionalSalePromotion, bool> predicate) => additionalSalePromotions.Where(predicate));     
            
        }

        [TestMethod]
        public void Service_Layer_Integration_Test()
        {
            //Arrange
            String[] basket = new String[] { "Apple", "Banana", "Orange", "Banana", "Orange", "Banana" };
            ISaleService saleService = new SaleServiceImpl(
                productRepository.Object,
                onSalePromotionRepository.Object,
                groupSalePromotionRepository.Object,
                additionalSalePromotionRepository.Object);

            
            foreach (var product in basket)
            {
                saleService.AddProduct(product);
            }

            //Act & Assert
            Assert.AreEqual(6.75m, saleService.GetReceipt().GetTotalRegularPrice());



            //Arrange Promotions
            onSalePromotions.Add(new OnSalePromotion() { Id = "Apple", OnSalePrice = 0.6m, Description = "Apple 60c", StartDate = DateTime.Now.AddMonths(-6), EndDate = DateTime.Now.AddMonths(6) });
            //Act & Assert
            Assert.AreEqual(6.60m, saleService.GetReceipt().GetTotalPayment());


            //Arrange Promotions
            groupSalePromotions.Add(new GroupSalePromotion() { Id = "Banana", Description = "Banana $2 for 3", GroupQuantity = 3, GroupPrice = 2.0m, StartDate = DateTime.Now.AddMonths(-6), EndDate = DateTime.Now.AddMonths(6) });
            //Act & Assert
            Assert.AreEqual(5.60m, saleService.GetReceipt().GetTotalPayment());


            //Arrange Promotions
            additionalSalePromotions.Add(new AdditionalSalePromotion() { Id = "Orange", BuyQuantity = 1, Description = "Orange BOGO", LastProductDiscountPercent = 0, StartDate = DateTime.Now.AddMonths(-6), EndDate = DateTime.Now.AddMonths(6) });
            //Act & Assert
            Assert.AreEqual(4.10m, saleService.GetReceipt().GetTotalPayment());


            //Banana OnSale and GroupSale are mixed. Let's find best price for customer
            //Arrange Promotions
            onSalePromotions.Add(new OnSalePromotion() { Id = "Banana", OnSalePrice = 0.8m, Description = "Banana 80c", StartDate = DateTime.Now.AddMonths(-6), EndDate = DateTime.Now.AddMonths(6) });
            //Act & Assert
            Assert.AreEqual(4.10m, saleService.GetReceipt().GetTotalPayment());


        }
    }
}
