﻿using OnlineStore_Identity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace OnlineStore_Identity.ViewModels
{
    public class ProductBillVM
    {
        //public List<Product> products = new List<Product>();
        public IEnumerable<Product> products;
        
        //public Bill bills = new Bill();

        public double todayProfit=0;

        public double allProfit=0;
    }
}
