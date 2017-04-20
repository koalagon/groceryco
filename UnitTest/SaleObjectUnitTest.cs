using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Domain.Models.Sales;
using Domain.Models.Products;
using Domain.Models.Promotions;

namespace UnitTest
{
    [TestClass]
    public class SaleObjectUnitTest
    {
        [TestMethod]
        public void Get_Regular_Total()
        {
            //Arrange
            Sale sale = new Sale();
            sale.AddProduct(new Product() { Id = "Apple", Price = 0.75m });
            sale.AddProduct(new Product() { Id = "Banana", Price = 1.00m });
            sale.AddProduct(new Product() { Id = "Orange", Price = 1.50m });

            //Act
            decimal total = sale.GetRegularTotal();

            //Assert
            Assert.AreEqual(3.25m, total);
        }

        [TestMethod]
        public void Get_Regular_Total_With_Duplicate_Products()
        {
            //Arrange
            Sale sale = new Sale();
            sale.AddProduct(new Product() { Id = "Apple", Price = 0.75m });
            sale.AddProduct(new Product() { Id = "Banana", Price = 1.00m });
            sale.AddProduct(new Product() { Id = "Orange", Price = 1.50m });
            sale.AddProduct(new Product() { Id = "Banana", Price = 1.00m });

            //Act
            decimal total = sale.GetRegularTotal();

            //Assert
            Assert.AreEqual(4.25m, total);
        }


        [TestMethod]
        public void Get_Discount_Total_With_On_Sale_Promotion()
        {
            //Arrange
            Sale sale = new Sale();
            sale.AddProduct(new Product() { Id = "Apple", Price = 0.75m });
            sale.AddProduct(new Product() { Id = "Banana", Price = 1.00m });
            sale.AddProduct(new Product() { Id = "Orange", Price = 1.50m });
            sale.AddProduct(new Product() { Id = "Banana", Price = 1.00m });

            sale.AddPromotion(new OnSalePromotion("Apple", DateTime.Now.AddMonths(-6), DateTime.Now.AddMonths(6), "Apple is on-sale", 0.5m));


            //Act
            decimal total1 = sale.GetPromotionTotal();

            //Assert
            Assert.AreEqual(4.00m, total1);


            //More Arrage
            sale.AddPromotion(new OnSalePromotion("Banana", DateTime.Now.AddMonths(-6), DateTime.Now.AddMonths(6), "Banana is on-sale", 0.5m));

            //Act
            decimal total2 = sale.GetPromotionTotal();

            //Assert
            Assert.AreEqual(3.00m, total2);
        }


        [TestMethod]
        public void Get_Discount_Total_With_Group_Sale_Promotion()
        {
            //Arrange
            Sale sale = new Sale();
            sale.AddProduct(new Product() { Id = "Banana", Price = 1.00m });
            sale.AddProduct(new Product() { Id = "Banana", Price = 1.00m });
            sale.AddProduct(new Product() { Id = "Banana", Price = 1.00m });
            sale.AddProduct(new Product() { Id = "Banana", Price = 1.00m });
            sale.AddProduct(new Product() { Id = "Banana", Price = 1.00m });

            sale.AddPromotion(new GroupSalePromotion("Banana", DateTime.Now.AddMonths(-6), DateTime.Now.AddMonths(6), "Banana has group-sale", 3, 2m));


            //Act
            decimal total = sale.GetPromotionTotal();

            //Assert
            Assert.AreEqual(4.00m, total);

        }


        [TestMethod]
        public void Get_Discount_Total_With_Additional_Sale_Promotion()
        {
            //Arrange
            Sale sale = new Sale();
            sale.AddProduct(new Product() { Id = "Banana", Price = 1.00m });
            sale.AddProduct(new Product() { Id = "Banana", Price = 1.00m });
            sale.AddProduct(new Product() { Id = "Banana", Price = 1.00m });
            sale.AddProduct(new Product() { Id = "Banana", Price = 1.00m });
            sale.AddProduct(new Product() { Id = "Banana", Price = 1.00m });
            

            sale.AddPromotion(new AdditionalSalePromotion("Banana", DateTime.Now.AddMonths(-6), DateTime.Now.AddMonths(6), "Banana has buy one get one free sale", 1, 0f));


            //Act
            decimal total1 = sale.GetPromotionTotal();

            //Assert
            Assert.AreEqual(3.00m, total1);



            //Rearrange
            sale.AddProduct(new Product() { Id = "Banana", Price = 1.00m });

            //Act
            decimal total2 = sale.GetPromotionTotal();

            //Assert
            Assert.AreEqual(3.00m, total2);



            //Rearrange
            sale.ClearPromotions();
            sale.AddPromotion(new AdditionalSalePromotion("Banana", DateTime.Now.AddMonths(-6), DateTime.Now.AddMonths(6), "Banana has buy one get 50% off free sale", 1, 0.5f));

            //Act
            decimal total3 = sale.GetPromotionTotal();

            //Assert
            Assert.AreEqual(4.50m, total3);



            //Rearrange
            sale.ClearPromotions();
            sale.AddPromotion(new AdditionalSalePromotion("Banana", DateTime.Now.AddMonths(-6), DateTime.Now.AddMonths(6), "Banana has buy two get one 50% off sale", 2, 0.5f));

            //Act
            decimal total4 = sale.GetPromotionTotal();

            //Assert
            Assert.AreEqual(5.00m, total4);
        }
    }
}
