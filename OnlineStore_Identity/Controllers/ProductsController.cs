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
        [Authorize]
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
        [Authorize]
        public IActionResult AddOrEdit(int id = 0)
        {
            if(id == 0)
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
        [Authorize]
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
        [Authorize]
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

        [Authorize]
          public IActionResult salesChart()
        {
            return PartialView();
        }
    }
}
