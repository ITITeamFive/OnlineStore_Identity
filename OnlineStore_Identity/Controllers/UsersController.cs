using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OnlineStore_Identity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace OnlineStore_Identity.Controllers
{
    public class UsersController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;

        HttpClient client = new HttpClient();

        public UsersController(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public class OrderRootObject
        {
            public string Metadata { get; set; }
            public IEnumerable<Bill> Value { get; set; }
           
        }

        public IActionResult Index()
        {
            string userID = _userManager.GetUserId(User);
            HttpResponseMessage response = client.GetAsync($"http://shirleyomda-001-site1.etempurl.com/odata/Bills?$expand=Payment&$filter=userID eq '{userID}'").Result;
            string order = response.Content.ReadAsStringAsync().Result;
            OrderRootObject myOrder = JsonConvert.DeserializeObject<OrderRootObject>(order);
           
            return View(myOrder.Value);
        }

        public IActionResult Orders()
        {
            string userID = _userManager.GetUserId(User);
            HttpResponseMessage response = client.GetAsync($"http://shirleyomda-001-site1.etempurl.com/odata/Bills?$expand=Payment&$filter=userID eq '{userID}'").Result;
            string order = response.Content.ReadAsStringAsync().Result;
            OrderRootObject myOrder = JsonConvert.DeserializeObject<OrderRootObject>(order);
            return PartialView(myOrder);
        }
    }
}
