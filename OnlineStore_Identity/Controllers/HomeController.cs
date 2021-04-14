using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using OnlineStore_Identity.Models;
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


        public class RootObject
        {

            public string Metadata { get; set; }
            public List<Product> Value { get; set; }

        }

        public class Root
        {
            public string metadata { get; set; }

            public int productID { get; set; }
            public string productName { get; set; }
            public string productBrand { get; set; }
            public string productMaterial { get; set; }
            public double productPrice { get; set; }
            public double productDiscount { get; set; }
            public string productDescription { get; set; }
            [JsonIgnore]
            public int classID { get; set; }
            [JsonIgnore]

            public int categoryID { get; set; }
        }


        HttpClient client = new HttpClient();

        public IActionResult Index()
        {

            HttpResponseMessage response = client.GetAsync("http://shirleyomda-001-site1.etempurl.com/odata/Products").Result;
            string Result = response.Content.ReadAsStringAsync().Result;
            RootObject products = JsonConvert.DeserializeObject<RootObject>(Result);
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
    }
}
