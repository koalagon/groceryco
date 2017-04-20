using Domain.Exceptions;
using Domain.Models.Products;
using Domain.Models.Promotions;
using Domain.Models.Sales;
using Domain.Repositories;
using Domain.Services;
using Infrastructure.Repositories;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kiosk
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true) // Loop indefinitely
            {
                Console.WriteLine("Press any key to checkout..."); 
                Console.ReadLine(); 
                
                Program program = new Program();
                program.Checkout();
            }

            
        }

        public void Checkout()
        {
            IRepository<Product> productRepository = new FileRepository<Product>(Path.Combine("..\\..\\Files", "product.json"));

            IRepository<OnSalePromotion> onSalePromotionRepository = 
                new FileRepository<OnSalePromotion>(Path.Combine("..\\..\\Files", "onsale.json"));

            IRepository<GroupSalePromotion> groupSalePromotionRepository = 
                new FileRepository<GroupSalePromotion>(Path.Combine("..\\..\\Files", "groupsale.json"));

            IRepository<AdditionalSalePromotion> additionalSalePromotionRepository = 
                new FileRepository<AdditionalSalePromotion>(Path.Combine("..\\..\\Files", "additionalsale.json"));

            // Service Layer Facade
            ISaleService saleService = new SaleServiceImpl(productRepository, onSalePromotionRepository, groupSalePromotionRepository, additionalSalePromotionRepository);

            string filePath = Path.Combine("..\\..\\Files", "basket.json");
            List<Product> productsInBasket = JsonConvert.DeserializeObject<List<Product>>(File.ReadAllText(filePath));

            try
            {
                foreach (var item in productsInBasket)
                {
                    saleService.AddProduct(item.Id);
                }

                Receipt receipt = saleService.GetReceipt();

                ReceiptPrinter receiptPrinter = new ConsoleReceiptPrinter(receipt);
                receiptPrinter.Print();
            }
            catch (ProductException pex)
            {
                Console.WriteLine("=== " + pex.ToString() + " ===");
            }
            catch (Exception ex)
            {
                Console.WriteLine("=== Woops, there's something wrong..." + ex.ToString() + " ===");
            }

        }
    }
}
