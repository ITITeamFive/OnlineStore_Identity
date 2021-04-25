using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OnlineStore_Identity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.OData;

namespace OnlineStore_Identity.Controllers
{
    public class CartsController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;

        public CartsController(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        HttpClient client = new HttpClient();
        public class RootObject<T>
        {
            public string Metadata { get; set; }
            public IEnumerable<T> Value { get; set; }
        }

        public IActionResult Index()
        {
            string userId = _userManager.GetUserId(User);
            HttpResponseMessage response = client.GetAsync($"http://shirleyomda-001-site1.etempurl.com/odata/Carts?$expand=Store/Product/Category&$filter=userID eq '{userId}'").Result;
            string cart = response.Content.ReadAsStringAsync().Result;
            RootObject<Cart> carts = JsonConvert.DeserializeObject <RootObject<Cart>>(cart);
            return View(carts.Value);
        }

        public IActionResult CartList()
        {
            string userId = _userManager.GetUserId(User);
            HttpResponseMessage response = client.GetAsync($"http://shirleyomda-001-site1.etempurl.com/odata/Carts?$expand=Store/Product/Category&$filter=userID eq '{userId}'").Result;
            string cart = response.Content.ReadAsStringAsync().Result;
            RootObject<Cart> carts = JsonConvert.DeserializeObject<RootObject<Cart>>(cart);
            return PartialView(carts.Value);
        }

        public IActionResult AddToCart(int id,int quantity)
        {
            string userId = _userManager.GetUserId(User);

            if (userId != null)
            {
                Cart cart = new Cart() {storeID=id,quantity=quantity,userID=userId};
                string _cart = JsonConvert.SerializeObject(cart);
                StringContent request = new StringContent(_cart,Encoding.UTF8, "application/json");
                HttpResponseMessage response = client.PostAsync($"http://shirleyomda-001-site1.etempurl.com/odata/Carts",request).Result;
                return Ok("Success");
            }
            return BadRequest("Error");
            //return RedirectToAction("Index");
        }


        public IActionResult RemoveFromCart(int id)
        {
            string userID = _userManager.GetUserId(User);
            HttpResponseMessage response = client.GetAsync($"http://shirleyomda-001-site1.etempurl.com/odata/Carts?$filter=userID eq '{userID}' and storeID eq {id}").Result;
            string cartResult = response.Content.ReadAsStringAsync().Result;
            RootObject<Cart> carts = JsonConvert.DeserializeObject<RootObject<Cart>>(cartResult);
            int cartID = carts.Value.Select(w => w.cartID).FirstOrDefault();

            HttpResponseMessage response2 = client.DeleteAsync($"http://shirleyomda-001-site1.etempurl.com/odata/Carts({cartID})").Result;
            return RedirectToAction("CartList");
        }
    }
}
