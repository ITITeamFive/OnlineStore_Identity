using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OnlineStore_Identity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using OnlineStore_Identity.ViewModels;
using System.Xml.Serialization;

namespace OnlineStore_Identity.Controllers
{
    public class ProductsController : Controller
    {
        public class RootObject<T>
        {
            public string Metadata { get; set; }
            public IEnumerable<T> Value { get; set; }
        }

        HttpClient client = new HttpClient();
       
        public IActionResult DashBoard()
        {
            ProductBillVM productBillVM = new ProductBillVM();

            HttpResponseMessage response = client.GetAsync("http://shirleyomda-001-site1.etempurl.com/odata/Products").Result;
            string Result = response.Content.ReadAsStringAsync().Result;
            RootObject<Product> products = JsonConvert.DeserializeObject<RootObject<Product>>(Result);
            productBillVM.products = products.Value;

            HttpResponseMessage response2 = client.GetAsync("http://shirleyomda-001-site1.etempurl.com/odata/Bills").Result;
            string Result2 = response2.Content.ReadAsStringAsync().Result;
            RootObject<Bill> Bills = JsonConvert.DeserializeObject<RootObject<Bill>>(Result2);
            IEnumerable<Bill> myBill = Bills.Value.Where(b => Convert.ToDateTime(b.billDate) == DateTime.Now.Date);

            foreach (Bill bill in myBill)
            {
                productBillVM.todayProfit += (double)bill.billTotal;
            }

            IEnumerable<Bill> allBills = Bills.Value;
            foreach (Bill bill in allBills)
            {
                productBillVM.allProfit += (double)bill.billTotal;
            }
            return View(productBillVM);
        }

       public IActionResult productsIndex()
        {
            return PartialView();
        }

        public IActionResult salesChart()
        {
            return PartialView();
        }
    }
}
