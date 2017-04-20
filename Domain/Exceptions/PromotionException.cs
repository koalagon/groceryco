using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class PromotionException : Exception
    {
        public PromotionException(string message)
            : base(message)
        {

        }
    }
}
