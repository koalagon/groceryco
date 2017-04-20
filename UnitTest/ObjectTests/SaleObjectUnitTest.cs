using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Domain.Models.Sales;
using Domain.Models.Products;
using Domain.Models.Promotions;

namespace UnitTest.ObjectTests
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
            decimal total = sale.CreateReceipt().GetTotalPayment();

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
            decimal total = sale.CreateReceipt().GetTotalPayment();

            //Assert
            Assert.AreEqual(4.25m, total);
        }


        [TestMethod]
        public void Get_Discounted_Total_With_On_Sale_Promotion()
        {
            //Arrange
            Sale sale = new Sale();
            sale.AddProduct(new Product() { Id = "Apple", Price = 0.75m });
            sale.AddProduct(new Product() { Id = "Banana", Price = 1.00m });
            sale.AddProduct(new Product() { Id = "Orange", Price = 1.50m });
            sale.AddProduct(new Product() { Id = "Banana", Price = 1.00m });

            sale.ApplyPromotion(new OnSalePromotion("Apple", DateTime.Now.AddMonths(-6), DateTime.Now.AddMonths(6), "Apple is on-sale", 0.5m));
            
            //Act
            decimal total = sale.CreateReceipt().GetTotalPayment();
                        
            //Assert
            Assert.AreEqual(4.00m, total);


            //More Arrage
            sale.ApplyPromotion(new OnSalePromotion("Banana", DateTime.Now.AddMonths(-6), DateTime.Now.AddMonths(6), "Banana is on-sale", 0.5m));


            //Act
            total = sale.CreateReceipt().GetTotalPayment();

            //Assert
            Assert.AreEqual(3.00m, total);
        }


        [TestMethod]
        public void Get_Discounted_Total_With_Expired_On_Sale_Promotion()
        {
            //Arrange
            Sale sale = new Sale();
            sale.AddProduct(new Product() { Id = "Apple", Price = 0.75m });
            sale.AddProduct(new Product() { Id = "Banana", Price = 1.00m });
            sale.AddProduct(new Product() { Id = "Orange", Price = 1.50m });
            sale.AddProduct(new Product() { Id = "Banana", Price = 1.00m });

            sale.ApplyPromotion(new OnSalePromotion("Apple", DateTime.Now.AddMonths(-12), DateTime.Now.AddMonths(-6), "Apple is on-sale", 0.5m));


            //Act
            decimal total = sale.CreateReceipt().GetTotalPayment();

            //Assert
            Assert.AreEqual(4.25m, total);

        }


        [TestMethod]
        public void Get_Discounted_Total_With_Group_Sale_Promotion()
        {
            //Arrange
            Sale sale = new Sale();
            sale.AddProduct(new Product() { Id = "Banana", Price = 1.00m });
            sale.AddProduct(new Product() { Id = "Banana", Price = 1.00m });
            

            sale.ApplyPromotion(new GroupSalePromotion("Banana", DateTime.Now.AddMonths(-6), DateTime.Now.AddMonths(6), "Banana has group-sale", 3, 2m));


            //Act
            decimal total = sale.CreateReceipt().GetTotalPayment();

            //Assert
            Assert.AreEqual(2.00m, total);


            // Reassign
            sale.AddProduct(new Product() { Id = "Banana", Price = 1.00m });
            sale.AddProduct(new Product() { Id = "Banana", Price = 1.00m });
            sale.AddProduct(new Product() { Id = "Banana", Price = 1.00m });
            //Act
            total = sale.CreateReceipt().GetTotalPayment();

            //Assert
            Assert.AreEqual(4.00m, total);


            // Reassign
            sale.ClearPromotions();
            sale.ApplyPromotion(new GroupSalePromotion("Banana", DateTime.Now.AddMonths(-6), DateTime.Now.AddMonths(6), "Banana has group-sale", 10, 2m));

            //Act
            total = sale.CreateReceipt().GetTotalPayment();

            //Assert
            Assert.AreEqual(5.00m, total);
        }


        [TestMethod]
        public void Get_Discounted_Total_With_Additional_Sale_Promotion()
        {
            //Arrange
            Sale sale = new Sale();
            sale.AddProduct(new Product() { Id = "Banana", Price = 1.00m });
            sale.AddProduct(new Product() { Id = "Banana", Price = 1.00m });
            sale.AddProduct(new Product() { Id = "Banana", Price = 1.00m });
            sale.AddProduct(new Product() { Id = "Banana", Price = 1.00m });
            sale.AddProduct(new Product() { Id = "Banana", Price = 1.00m });
            

            sale.ApplyPromotion(new AdditionalSalePromotion("Banana", DateTime.Now.AddMonths(-6), DateTime.Now.AddMonths(6), "Banana has buy one get one free sale", 1, 0f));


            //Act
            decimal total = sale.CreateReceipt().GetTotalPayment();

            //Assert
            Assert.AreEqual(3.00m, total);



            //Rearrange
            sale.AddProduct(new Product() { Id = "Banana", Price = 1.00m });

            //Act
            total = sale.CreateReceipt().GetTotalPayment();

            //Assert
            Assert.AreEqual(3.00m, total);



            //Rearrange
            sale.ClearPromotions();
            sale.ApplyPromotion(new AdditionalSalePromotion("Banana", DateTime.Now.AddMonths(-6), DateTime.Now.AddMonths(6), "Banana has buy one get 50% off free sale", 1, 0.5f));

            //Act
            total = sale.CreateReceipt().GetTotalPayment();

            //Assert
            Assert.AreEqual(4.50m, total);



            //Rearrange
            sale.ClearPromotions();
            sale.ApplyPromotion(new AdditionalSalePromotion("Banana", DateTime.Now.AddMonths(-6), DateTime.Now.AddMonths(6), "Banana has buy two get one 50% off sale", 2, 0.5f));

            //Act
            total = sale.CreateReceipt().GetTotalPayment();

            //Assert
            Assert.AreEqual(5.00m, total);
        }



        [TestMethod]
        public void Get_Discounted_Total_With_Mixed_Promotion()
        {
            //Arrange
            Sale sale = new Sale();
            sale.AddProduct(new Product() { Id = "Apple", Price = 0.75m });
            sale.AddProduct(new Product() { Id = "Apple", Price = 0.75m });
            sale.AddProduct(new Product() { Id = "Apple", Price = 0.75m });

            sale.ApplyPromotion(new OnSalePromotion("Apple", DateTime.Now.AddMonths(-6), DateTime.Now.AddMonths(6), "Apple is on-sale", 0.7m));
            sale.ApplyPromotion(new GroupSalePromotion("Apple", DateTime.Now.AddMonths(-6), DateTime.Now.AddMonths(6), "Apple group sale", 2, 1m));
            //Act
            decimal total = sale.CreateReceipt().GetTotalPayment();

            //Assert
            Assert.AreEqual(1.75m, total);


        }
    }
}
