using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Promotions
{
    public class PricingStrategyFactory
    {
        private static PricingStrategyFactory pricingStrategyFactory;

        private PricingStrategyFactory()
        {

        }

        public static PricingStrategyFactory NewInstance()
        {
            if (pricingStrategyFactory == null)
                pricingStrategyFactory = new PricingStrategyFactory();

            return pricingStrategyFactory;
        }


        public IPricingStrategy CreateStrategy(string strategyType)
        {
            switch (strategyType)
            {
                case "highest":
                    return new HighestPricingStrategy();
                default:
                    return new LowestPricingStrategy();
            }
        }

    }
}
