using Marten.Storage;
using Microsoft.AspNetCore.Mvc;
using OnlineStore_Identity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore_Identity.Controllers
{
    public class BillsController : Controller
    {
        public IActionResult Index(int shippingID,int phone,string addressDetails,int paymentID,int tempTotal,int total)
        {
            //POST//Address => Payment => Bill => BillProduct

            //StoreID To get all carts Products(For BillProduct)
            //foreach to post billProduct

            //return partial view and design modal
            return View();
        }
    }
}
