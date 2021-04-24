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
    public class WishlistsController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;

        public WishlistsController(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public class RootObject<T>
        {
            public string Metadata { get; set; }
            public IEnumerable<T> Value { get; set; }
        }

        HttpClient client = new HttpClient();

        public IActionResult Index()
        {
            string userId = _userManager.GetUserId(User);

            HttpResponseMessage response = client.GetAsync($"http://shirleyomda-001-site1.etempurl.com/odata/WishLists?$expand=Product&$filter=userID eq '{userId}'").Result;
            string wishResult = response.Content.ReadAsStringAsync().Result;
            RootObject<WishList> wishlists = JsonConvert.DeserializeObject<RootObject<WishList>>(wishResult);
           
            HttpResponseMessage response2 = client.GetAsync($"http://shirleyomda-001-site1.etempurl.com/odata/Categories").Result;
            string catResult = response2.Content.ReadAsStringAsync().Result;
            RootObject<Category> catlists = JsonConvert.DeserializeObject<RootObject<Category>>(catResult);
            ViewBag.catIDs = catlists.Value;
           
            HttpResponseMessage response3 = client.GetAsync($"http://shirleyomda-001-site1.etempurl.com/odata/Reviews").Result;
            string rateResult = response3.Content.ReadAsStringAsync().Result;
            RootObject<Review> ratelists = JsonConvert.DeserializeObject<RootObject<Review>>(rateResult);
            ViewBag.review = ratelists.Value;

            return View(wishlists.Value);
        }

        public IActionResult RemoveFromWishlist(int id)
        {
            if (id != 0)
            {
                HttpResponseMessage response=client.DeleteAsync($"http://shirleyomda-001-site1.etempurl.com/odata/WishLists({id})").Result;
            }
            return RedirectToAction("Wishlist");
        }

        public IActionResult Wishlist()
        {
            string userId = _userManager.GetUserId(User);

            HttpResponseMessage response = client.GetAsync($"http://shirleyomda-001-site1.etempurl.com/odata/WishLists?$expand=Product&$filter=userID eq '{userId}'").Result;
            string wishResult = response.Content.ReadAsStringAsync().Result;
            RootObject<WishList> wishlists = JsonConvert.DeserializeObject<RootObject<WishList>>(wishResult);

            HttpResponseMessage response2 = client.GetAsync($"http://shirleyomda-001-site1.etempurl.com/odata/Categories").Result;
            string catResult = response2.Content.ReadAsStringAsync().Result;
            RootObject<Category> catlists = JsonConvert.DeserializeObject<RootObject<Category>>(catResult);
            ViewBag.catIDs = catlists.Value;

            HttpResponseMessage response3 = client.GetAsync($"http://shirleyomda-001-site1.etempurl.com/odata/Reviews").Result;
            string rateResult = response3.Content.ReadAsStringAsync().Result;
            RootObject<Review> ratelists = JsonConvert.DeserializeObject<RootObject<Review>>(rateResult);
            ViewBag.review = ratelists.Value;

            return PartialView(wishlists.Value);
        }

     
    }
}
