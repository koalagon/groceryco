using Domain.Models.Promotions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Sales
{
    public class ConsoleReceiptPrinter : ReceiptPrinter
    {
        readonly Receipt receipt;
        readonly StringBuilder sb = new StringBuilder();

        public ConsoleReceiptPrinter(Receipt receipt)
        {
            this.receipt = receipt;
        }

        private void PrintHeader()
        {
            Console.WriteLine("==============================================================================");
            Console.WriteLine("=======================   GroceryCo Checkout Receipt   =======================");
            Console.WriteLine("==============================================================================");
        }

        private void PrintRegularPrice()
        {
            Console.WriteLine(string.Format("{0}{1}{2}{3}", "Product".PadRight(29), "Quantity".PadRight(20), "Price".PadRight(20), "Sub Total"));
            foreach (var item in receipt.GetSaleLineItems())
            {
                Console.WriteLine(string.Format("{0}{1}{2}{3}",
                    item.Product.Id.PadRight(29), item.Quantity.ToString().PadRight(20), item.Product.Price.ToString().PadRight(20), item.GetSubTotal()));
            }

            Console.WriteLine("\n*** Regular Price Grand Total: " + receipt.GetTotalRegularPrice() + "\n");
        }

        private void PrintAppliedPromotion()
        {
            Console.WriteLine("========================   GroceryCo Promotion List   ========================");

            Console.WriteLine(string.Format("{0}{1}{2}{3}", "Promotion".PadRight(29), "Regular Price".PadRight(20), "Discounted Price".PadRight(20), "Saved"));
            var appliedPromotions = receipt.GetAppliedPromotions();

            foreach (var appliedPromotion in appliedPromotions)
            {
                Console.WriteLine(string.Format("{0}{1}{2}{3}",
                    appliedPromotion.GetPromotion().Description.PadRight(29),
                    appliedPromotion.GetRegularPrice().ToString().PadRight(20),
                    appliedPromotion.GetDiscountPrice().ToString().PadRight(20),
                    appliedPromotion.GetDiscountAmount()));                    
            }

            Console.WriteLine("\n*** Saved Money Grand Total: " + receipt.GetTotalSavedMoney() + "\n");
        }

        private void PrintPayment()
        {
            Console.WriteLine("\n*** Total Due Payment: " + receipt.GetTotalPayment() + "\n\n");
        }

        public override void Print()
        {
            PrintHeader();
            PrintRegularPrice();
            PrintAppliedPromotion();
            PrintPayment();
        }
    }
}
