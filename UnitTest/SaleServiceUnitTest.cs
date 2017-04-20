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

namespace UnitTest
{
    [TestClass]
    public class SaleServiceUnitTest
    {
        IEnumerable<Product> products;
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

            groupSalePromotionRepository = new Mock<IRepository<GroupSalePromotion>>();

            additionalSalePromotionRepository = new Mock<IRepository<AdditionalSalePromotion>>();
        }

        [TestMethod]
        public void Get_Regular_Price_Total()
        {
            //Arrange
            String[] basket = new String[] { "Apple", "Banana", "Orange", "Banana", "Orange", "Banana" };
            ISaleService saleService = new SaleServiceImpl(
                productRepository.Object,
                onSalePromotionRepository.Object,
                groupSalePromotionRepository.Object,
                additionalSalePromotionRepository.Object);

            //Act
            foreach (var product in basket)
            {
                saleService.AddProduct(product);
            }

            //Assert
            Assert.AreEqual(6.75m, saleService.GetRegularPriceTotal());
        }

        [TestMethod]
        public void Get_Discount_Price_Total()
        {
            //Arrange
            String[] basket = new String[] { "Apple", "Banana", "Orange", "Banana", "Orange", "Banana" };
            ISaleService saleService = new SaleServiceImpl(
                productRepository.Object,
                onSalePromotionRepository.Object,
                groupSalePromotionRepository.Object,
                additionalSalePromotionRepository.Object);

            //Act
            foreach (var product in basket)
            {
                saleService.AddProduct(product);
            }


        }

    }
}
