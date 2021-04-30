using Marten.Storage;
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

namespace OnlineStore_Identity.Controllers
{
    public class BillsController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;

        HttpClient client = new HttpClient();

        public BillsController(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }
        public class RootObject<T>
        {
            public string Metadata { get; set; }
            public IEnumerable<T> Value { get; set; }
        }

        public class RootObject
        {
            public string Metadata { get; set; }
            public int addressID { get; set; }
            public int billID { get; set; }
            public List<Cart> Value { get; set; }

        }

        public IActionResult Index(int shippingID,int phone,string addressDetails,int paymentID,int tempTotal,int total)
        {
            //POST//Address => Payment => Bill => BillProduct
            string userID = _userManager.GetUserId(User);

            #region Address
            Address address = new Address() { shippingID = shippingID, addressDetails = addressDetails, addressPhone = phone };
            string _address = JsonConvert.SerializeObject(address);
            StringContent request = new StringContent(_address, Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PostAsync($"http://shirleyomda-001-site1.etempurl.com/odata/Addresses", request).Result;
            var myAddress = response.Content.ReadAsStringAsync().Result;
            RootObject addressRoot = JsonConvert.DeserializeObject<RootObject>(myAddress);
            int addressID = addressRoot.addressID;
            #endregion

            #region Bill
            Bill bill = new Bill() { paymentID = paymentID, billTotal = total, billSubTotal = tempTotal, billDate = DateTime.Now, userID = userID, addressID = addressID };
            string _bill = JsonConvert.SerializeObject(bill);
            StringContent request2 = new StringContent(_bill, Encoding.UTF8, "application/json");
            HttpResponseMessage response2 = client.PostAsync($"http://shirleyomda-001-site1.etempurl.com/odata/Bills", request2).Result;
            var myBill = response2.Content.ReadAsStringAsync().Result;
            RootObject billRoot = JsonConvert.DeserializeObject<RootObject>(myBill);
            int billID = billRoot.billID;
            #endregion

            #region BillProduct
            //StoreID To get all carts Products(For BillProduct)
            //foreach to post billProduct
            HttpResponseMessage response4 = client.GetAsync($"http://shirleyomda-001-site1.etempurl.com/odata/Carts?$expand=Store/Product&$filter=userID eq '{userID}'").Result;
            string carts = response4.Content.ReadAsStringAsync().Result;
            RootObject cartList = JsonConvert.DeserializeObject<RootObject>(carts);
            List<Cart> myCarts = cartList.Value;
            foreach (var item in myCarts)
            {
                BillProduct billProduct = new BillProduct()
                {
                    billID = billID,
                    storeID = item.storeID,
                    productID = item.Store.productID,
                    billProductQuantity = item.quantity,
                    billProductPrice = (item.Store.Product.productPrice) * (1 - item.Store.Product.productDiscount / 100)
                };
                string _billProduct = JsonConvert.SerializeObject(billProduct);
                StringContent request3 = new StringContent(_billProduct, Encoding.UTF8, "application/json");
                HttpResponseMessage response3 = client.PostAsync($"http://shirleyomda-001-site1.etempurl.com/odata/BillProducts", request3).Result;
                HttpResponseMessage response5 = client.DeleteAsync($"http://shirleyomda-001-site1.etempurl.com/odata/Carts({item.cartID})").Result;

                #region Modifying store quantity after purchasing
                int removedQuantity = item.quantity ?? 0;
                int storeQuantity = item.Store.productQuantity ?? 0;
                storeQuantity -= removedQuantity;
                string _storeQuantityUpdate = JsonConvert.SerializeObject(new { productQuantity= storeQuantity });
                StringContent request6 = new StringContent(_storeQuantityUpdate, Encoding.UTF8, "application/json");
                HttpResponseMessage response6 = client.PatchAsync($"http://shirleyomda-001-site1.etempurl.com/odata/Stores({item.storeID})", request6).Result; 
                #endregion

            }
            #endregion

            //return partial view and design modal
            //return RedirectToAction("CartList", "Carts");
            return RedirectToAction("BillDetails");
        }

        public IActionResult BillDetails()
        {
            string userID = _userManager.GetUserId(User);
             
            HttpResponseMessage response=client.GetAsync($"http://shirleyomda-001-site1.etempurl.com/odata/Bills?$expand=Address,Payment&$filter=userID eq '{userID}'").Result;
            string bill = response.Content.ReadAsStringAsync().Result;
            RootObject<Bill> bills = JsonConvert.DeserializeObject<RootObject<Bill>>(bill);

            return View(bills.Value);
        }
    }
}
