﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Products
{
    public class Product
    {
        // Unique Identifier
        public string Id { get; set; }

        public decimal Price { get; set; }
    }
}
