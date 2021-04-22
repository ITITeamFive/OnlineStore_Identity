using OnlineStore_Identity.Models;
using OnlineStore_Identity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore_Identity.ViewModels
{
    public class ProductDetailsVM
    {
        public Product product; 
        public IEnumerable<Store> stores;
    }
}
