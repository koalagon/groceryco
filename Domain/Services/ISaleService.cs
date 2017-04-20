using Domain.Models.Products;
using Domain.Models.Promotions;
using Domain.Models.Sales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
    public interface ISaleService
    {
        void AddProduct(string id);
        Receipt GetReceipt();
    }
}
