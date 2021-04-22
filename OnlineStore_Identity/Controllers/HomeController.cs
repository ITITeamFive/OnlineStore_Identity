using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using OnlineStore_Identity.Models;
using OnlineStore_Identity.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace OnlineStore_Identity.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }


        public class RootObject<T>
        {
            public string Metadata { get; set; }
            public IEnumerable<T> Value { get; set; }
        }


        HttpClient client = new HttpClient();

        public IActionResult Index()
        {

            HttpResponseMessage response = client.GetAsync("http://shirleyomda-001-site1.etempurl.com/odata/Products").Result;
            string Result = response.Content.ReadAsStringAsync().Result;
            RootObject<Product> products = JsonConvert.DeserializeObject<RootObject<Product>>(Result);
            return View(products.Value);

            //Byte[] b = System.IO.File.ReadAllBytes(@"D:\ITI\Projects\Project4\New Project Online\OnlineStore_Identity\OnlineStore_Identity\wwwroot\css\assets\Images\img1.jpg");   // You can use your own method over here.         
            //return File(b, "image/jpeg");

            //return PhysicalFile(@"D:\ITI\Projects\Project4\New Project Online\OnlineStore_Identity\OnlineStore_Identity\wwwroot\css\assets\Images\img1.jpg", "image/jpeg");

        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult ProductDetails(int id)
        {
            HttpResponseMessage response = client.GetAsync($"http://shirleyomda-001-site1.etempurl.com/odata/Products({id})?$expand=Category,Class,Stores,Reviews").Result;
            string Result = response.Content.ReadAsStringAsync().Result;
            ProductDetailsVM productDetails  = JsonConvert.DeserializeObject<ProductDetailsVM>(Result);
            int rate = 0;
            if(productDetails.Reviews.Count != 0)
            {
                foreach (var review in productDetails.Reviews)
                {
                    rate += review.rate ?? 0;
                }
            }

            ViewBag.rate = rate;
            return PartialView(productDetails);
        }
    }
}
