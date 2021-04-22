using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using OnlineStore_Identity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using OnlineStore_Identity.ViewModels;
using System.Xml.Serialization;
using Microsoft.AspNetCore.Authorization;
using OnlineStore_Web_API.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace OnlineStore_Identity.Controllers
{
    public class ProductsController : Controller
    {
        public class RootObject<T>
        {
            public string Metadata { get; set; }
            public IEnumerable<T> Value { get; set; }
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
       [Authorize(Roles= "Admin")]
        public IActionResult DashBoard()
        {
            ProductBillVM productBillVM = new ProductBillVM();
            HttpResponseMessage response = client.GetAsync("http://shirleyomda-001-site1.etempurl.com/odata/Products").Result;
            string Result = response.Content.ReadAsStringAsync().Result;
            RootObject<Product> products = JsonConvert.DeserializeObject<RootObject<Product>>(Result);
            productBillVM.products = products.Value;

            HttpResponseMessage res3 = client.GetAsync("http://shirleyomda-001-site1.etempurl.com/odata/Stores").Result;
            string result3 = res3.Content.ReadAsStringAsync().Result;
            RootObject<Store> stores = JsonConvert.DeserializeObject<RootObject<Store>>(result3);
            productBillVM.stores = stores.Value;
            ViewBag.l = 0;
            HttpResponseMessage res4 = client.GetAsync("http://shirleyomda-001-site1.etempurl.com/odata/BillProducts").Result;
            string result4 = res4.Content.ReadAsStringAsync().Result;
            RootObject<BillProduct> billProducts  = JsonConvert.DeserializeObject<RootObject<BillProduct>>(result4);
            productBillVM.billProducts = billProducts.Value;

            //Get classes
            HttpResponseMessage res5 = client.GetAsync("http://shirleyomda-001-site1.etempurl.com/odata/Classes").Result;
            string result5 = res5.Content.ReadAsStringAsync().Result;
            RootObject<Class> classes = JsonConvert.DeserializeObject<RootObject<Class>>(result5);
            productBillVM.classes = classes.Value;

            //get categories
            HttpResponseMessage res6 = client.GetAsync("http://shirleyomda-001-site1.etempurl.com/odata/Categories").Result;
            string result6 = res6.Content.ReadAsStringAsync().Result;
            RootObject<Category> categories = JsonConvert.DeserializeObject<RootObject<Category>>(result6);
            productBillVM.categories = categories.Value;



            HttpResponseMessage response2 = client.GetAsync("http://shirleyomda-001-site1.etempurl.com/odata/Bills").Result;
            string Result2 = response2.Content.ReadAsStringAsync().Result;
            RootObject<Bill> Bills = JsonConvert.DeserializeObject<RootObject<Bill>>(Result2);

            /**** bills of last Month**/
            DateTime monthago = DateTime.Now.Date;
            DateTime thisYearBegin= new System.DateTime(DateTime.Now.Year,1,1, 0,0,0);
            if (DateTime.Now.Month == 1)
            {
                //monthago = new System.DateTime(DateTime.Now.Year - 1, 12,
                //DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
                monthago = new System.DateTime(DateTime.Now.Year - 1, 12,
              1, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            }
            else
            {
                //monthago = new System.DateTime(DateTime.Now.Year, DateTime.Now.Month-1,
                //  DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
                monthago = new System.DateTime(DateTime.Now.Year, DateTime.Now.Month - 1,
                  1, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            }

            IEnumerable<Bill> LastMonthBills = Bills.Value.Where(b => Convert.ToDateTime(b.billDate) > monthago);
            foreach (Bill bill in LastMonthBills)
            {
                productBillVM.lastMonthProfit[bill.billDate.Value.Day-1] += (double)bill.billTotal;
            }
            foreach (Bill bill in LastMonthBills)
            {
                productBillVM.lastYearProfit[bill.billDate.Value.Month-1] += (double)bill.billTotal;
            }
            IEnumerable<Bill> myBill = Bills.Value.Where(b => Convert.ToDateTime(b.billDate) == DateTime.Now.Date);

            productBillVM.bills = Bills.Value;
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
        //public IActionResult _productsIndex()
        //{
        //    HttpResponseMessage response = client.GetAsync("http://shirleyomda-001-site1.etempurl.com/odata/Products").Result;
        //    string Result = response.Content.ReadAsStringAsync().Result;
        //    RootObject products = JsonConvert.DeserializeObject<RootObject>(Result);
        //    ViewBag.Products = products.Value;
        //    List<Product> x = products.Value;
        //    return PartialView(x);
        //    //return PartialView();
        //}
        //[Authorize]
        public IActionResult productsIndex()
        {
            //HttpResponseMessage response = client.GetAsync("http://shirleyomda-001-site1.etempurl.com/odata/Products").Result;
            //string Result = response.Content.ReadAsStringAsync().Result;
            //RootObject<Product> products = JsonConvert.DeserializeObject<RootObject<Product>>(Result);
            //ViewBag.Products = products.Value;
            //List<Product> x = products.Value;
            //return PartialView(x);
            ProductBillVM productBillVM = new ProductBillVM();

            HttpResponseMessage response = client.GetAsync("http://shirleyomda-001-site1.etempurl.com/odata/Products").Result;
            string Result = response.Content.ReadAsStringAsync().Result;
            RootObject<Product> products = JsonConvert.DeserializeObject<RootObject<Product>>(Result);
            productBillVM.products = products.Value;

            HttpResponseMessage res3 = client.GetAsync("http://shirleyomda-001-site1.etempurl.com/odata/Stores").Result;
            string result3 = res3.Content.ReadAsStringAsync().Result;
            RootObject<Store> stores = JsonConvert.DeserializeObject<RootObject<Store>>(result3);
            productBillVM.stores = stores.Value;

            HttpResponseMessage res4 = client.GetAsync("http://shirleyomda-001-site1.etempurl.com/odata/BillProducts").Result;
            string result4 = res4.Content.ReadAsStringAsync().Result;
            RootObject<BillProduct> billProducts = JsonConvert.DeserializeObject<RootObject<BillProduct>>(result4);
            productBillVM.billProducts = billProducts.Value;

            //Get classes
            HttpResponseMessage res5 = client.GetAsync("http://shirleyomda-001-site1.etempurl.com/odata/Classes").Result;
            string result5 = res5.Content.ReadAsStringAsync().Result;
            RootObject<Class> classes = JsonConvert.DeserializeObject<RootObject<Class>>(result5);
            productBillVM.classes = classes.Value;

            //get categories
            HttpResponseMessage res6 = client.GetAsync("http://shirleyomda-001-site1.etempurl.com/odata/Categories").Result;
            string result6 = res6.Content.ReadAsStringAsync().Result;
            RootObject<Category> categories = JsonConvert.DeserializeObject<RootObject<Category>>(result6);
            productBillVM.categories = categories.Value;

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
            //return View(productBillVM);
            return PartialView(productBillVM);
        }

        // GET: OnlineStore_Identity/AddOrEdit(Insert)
        // GET: OnlineStore_Identity/AddOrEdit/5(Update)
        [HttpGet]
        [NoDirectAccess]
        //[Authorize]
        public IActionResult AddOrEdit(int id = 0)
        {
            //Get classes
            HttpResponseMessage res5 = client.GetAsync("http://shirleyomda-001-site1.etempurl.com/odata/Classes").Result;
            string result5 = res5.Content.ReadAsStringAsync().Result;
            RootObject<Class> classes = JsonConvert.DeserializeObject<RootObject<Class>>(result5);
            //ViewBag.classes = classes.Value;
            ViewBag.classes = new SelectList(classes.Value, "classID", "className");

            //get categories
            HttpResponseMessage res6 = client.GetAsync("http://shirleyomda-001-site1.etempurl.com/odata/Categories").Result;
            string result6 = res6.Content.ReadAsStringAsync().Result;
            RootObject<Category> categories = JsonConvert.DeserializeObject<RootObject<Category>>(result6);
            //ViewBag.categories = categories.Value;
            ViewBag.categories = new SelectList(categories.Value, "categoryID", "categoryName");

            if (id == 0)
            {
                return View(new Product());
            }
            else
            {
                //HttpResponseMessage response =  client.GetAsync($"http://shirleyomda-001-site1.etempurl.com/odata/Products({id}").Result;
                //string Result =  response.Content.ReadAsStringAsync().Result;
                //RootObject products = JsonConvert.DeserializeObject<RootObject>(Result);
                HttpResponseMessage response = client.GetAsync($"http://shirleyomda-001-site1.etempurl.com/odata/Products({id})").Result;
                string Result = response.Content.ReadAsStringAsync().Result;
                var products = JsonConvert.DeserializeObject<Root>(Result);
                Product x = new Product { productID = products.productID, productName = products.productName, productBrand = products.productBrand, productMaterial = products.productMaterial, productPrice = products.productPrice, productDiscount = products.productDiscount, productDescription = products.productDescription, classID = products.classID, categoryID = products.categoryID };
                return View(x);
            }
        }
        [Authorize]
        [HttpPost]
        public IActionResult AddOrEdit(int id,[Bind("productID,productName,productBrand,productDescription,productMaterial,productPrice,productDiscount,classID,categoryID")] Product _product)
        {
            if (ModelState.IsValid)
            {
                if(id == 0)
                {
                    string po = JsonConvert.SerializeObject(_product);
                    StringContent request = new StringContent(po, Encoding.UTF8, "application/json");
                    HttpResponseMessage content = client.PostAsync("http://shirleyomda-001-site1.etempurl.com/odata/Products", request).Result;
                    if (content.IsSuccessStatusCode)
                    {
                        HttpResponseMessage response = client.GetAsync("http://shirleyomda-001-site1.etempurl.com/odata/Products").Result;
                        string Result = response.Content.ReadAsStringAsync().Result;
                        RootObject<Product> products = JsonConvert.DeserializeObject<RootObject<Product>>(Result);
                        IEnumerable<Product> c = products.Value;
                        //ViewBag.Products = products.Value;
                        //return RedirectToAction("Index");
                        //return Json(new { isValid = true, html= Helper.RenderRazorViewToString(this, "productIndex", c) });
                        return RedirectToAction("productsIndex");
                    }
                }
                else
                {
                    //Product l = new Product { productID = 1, productName = "koko", productBrand = "koko", productMaterial = "koko", productPrice = 120, productDiscount = 120, productDescription = "koko", classID= null, categoryID= null};

                    string cha = JsonConvert.SerializeObject(_product);
                    //StringContent request = new StringContent(cha, Encoding.UTF8, "application/json");
                    HttpContent request = new StringContent(cha, Encoding.UTF8, "application/json");
                    HttpResponseMessage response =  client.PutAsync($"http://shirleyomda-001-site1.etempurl.com/odata/Products({id})", request).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        HttpResponseMessage respons = client.GetAsync("http://shirleyomda-001-site1.etempurl.com/odata/Products").Result;
                        string Result = respons.Content.ReadAsStringAsync().Result;
                        RootObject<Product> products = JsonConvert.DeserializeObject<RootObject<Product>>(Result);
                        IEnumerable<Product> x = products.Value;
                        //ViewBag.Products = products.Value;
                        //return RedirectToAction("Index");
                        //return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "productIndex", x) });
                        return RedirectToAction("productsIndex");
                    }
                }     
            }
            //return View(_product);
            //return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "AddOrEdit", _product) });
            return RedirectToAction("Dashboard");
        }
        //[Authorize]
        [HttpGet]
        [NoDirectAccess]
        public IActionResult Delete(int id)
        {
            HttpResponseMessage response = client.GetAsync($"http://shirleyomda-001-site1.etempurl.com/odata/Products({id})").Result;
            string Result = response.Content.ReadAsStringAsync().Result;
            var products = JsonConvert.DeserializeObject<Root>(Result);
            Product x = new Product { productID = products.productID, productName = products.productName, productBrand = products.productBrand, productMaterial = products.productMaterial, productPrice = products.productPrice, productDiscount = products.productDiscount, productDescription = products.productDescription, classID = products.classID, categoryID = products.categoryID };
            return View(x);
        }
        //[Authorize]
        [HttpPost]
        public IActionResult Delete(int id, Product pro)
        {
            HttpResponseMessage response = client.DeleteAsync($"http://shirleyomda-001-site1.etempurl.com/odata/Products({id})").Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("productsIndex");
                //HttpResponseMessage respons = client.GetAsync("http://shirleyomda-001-site1.etempurl.com/odata/Products").Result;
                //string Result = respons.Content.ReadAsStringAsync().Result;
                //RootObject products = JsonConvert.DeserializeObject<RootObject>(Result);
                //List<Product> x = products.Value;
                ////ViewBag.Products = products.Value;
                //return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "_productIndex", x) });
            }
            return View("Error");
        }
        //[Authorize]
          public IActionResult salesChart()
        {
            return PartialView();
        }
        public ActionResult Details(int id)
        {
            ProductDetailsVM productDetails = new ProductDetailsVM();
            HttpResponseMessage response3 = client.GetAsync("http://shirleyomda-001-site1.etempurl.com/odata/Stores").Result;
            string Result = response3.Content.ReadAsStringAsync().Result;
            RootObject<Store> stores = JsonConvert.DeserializeObject<RootObject<Store>>(Result);
            productDetails.stores = stores.Value.Where(b => b.productID == id);

            ProductBillVM productBillVM = new ProductBillVM();
            HttpResponseMessage response = client.GetAsync("http://shirleyomda-001-site1.etempurl.com/odata/Products").Result;
            Result = response.Content.ReadAsStringAsync().Result;
            RootObject<Product> products = JsonConvert.DeserializeObject<RootObject<Product>>(Result);
            productDetails.product = products.Value.FirstOrDefault(b=>b.productID==id);
            return View(productDetails);
        }

        [HttpGet]
        [NoDirectAccess]
        public IActionResult Deletestore(int id)
        {
            HttpResponseMessage response = client.GetAsync($"http://shirleyomda-001-site1.etempurl.com/odata/Stores").Result;
            string Result = response.Content.ReadAsStringAsync().Result;
            RootObject<Store> stores = JsonConvert.DeserializeObject<RootObject<Store>>(Result);
            Store x = stores.Value.FirstOrDefault(b => b.ID == id);
            response = client.DeleteAsync($"http://shirleyomda-001-site1.etempurl.com/odata/Stores({x.ID})").Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction($"Details", x.productID);
            }
            return View("Error");
        }

        [HttpGet]
        // GET: EmployeeController1/Create
        public ActionResult AddStore(int id)
        {
            ViewData["id"] = id;
            return View();
        }

        // POST: EmployeeController1/Create
        [HttpPost]
        public ActionResult AddStore(Store s)
        {
            string data = JsonConvert.SerializeObject(s);
            StringContent reqBody = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage responseMessage = client.PostAsync("http://shirleyomda-001-site1.etempurl.com/odata/Stores", reqBody).Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                ViewBag.Message = "added";
            }
            else
            {
                ViewBag.Message = "Error not added";
            }
                        return RedirectToAction("Details",s.productID);
            //return RedirectToAction(nameof(Details),s.productID);
        }
    }
}