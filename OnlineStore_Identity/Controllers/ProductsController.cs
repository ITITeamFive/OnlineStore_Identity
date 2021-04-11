using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OnlineStore_Identity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Kendo.Mvc.UI;


namespace OnlineStore_Identity.Controllers
{
    public class ProductsController : Controller
    {
        public class RootObject
        {
            
            public string Metadata { get; set; }
            public List<Product> Value { get; set; }
        }

        HttpClient client = new HttpClient();
        //public IActionResult Index([DataSourceRequest] DataSourceRequest request)
        //{
        //    HttpResponseMessage response = client.GetAsync("http://shirleyomda-001-site1.etempurl.com/odata/Products").Result;
        //    string Result = response.Content.ReadAsStringAsync().Result;
        //    //Json d = JsonConvert(Result);
        //    RootObject products = JsonConvert.DeserializeObject<RootObject>(Result);
        //    return View(products.Value);
        //    //return Json(products.Value.ToDataSourceResult(request));
        //}
        
        public IActionResult DashBoard()
        {
            HttpResponseMessage response = client.GetAsync("http://shirleyomda-001-site1.etempurl.com/odata/Products").Result;
            string Result = response.Content.ReadAsStringAsync().Result;
            RootObject products = JsonConvert.DeserializeObject<RootObject>(Result);
            ViewBag.Products = products.Value;
            return View();
        }

       public IActionResult productsIndex()
        {
            HttpResponseMessage response = client.GetAsync("http://shirleyomda-001-site1.etempurl.com/odata/Products").Result;
            string Result = response.Content.ReadAsStringAsync().Result;
            RootObject products = JsonConvert.DeserializeObject<RootObject>(Result);
            //ViewBag.Products = products.Value;
            return PartialView(products.Value);
        }
    }
}
