using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Example01.Context;

namespace Example01.Controllers
{
    public class ProductController : Controller
    {
        WebASPEntities1 objWebASPEntities = new WebASPEntities1();
        // GET: Product
        public ActionResult Detail(int id)
        {
            var objProduct = objWebASPEntities.Product_2119110193.Where(n => n.Id == id).FirstOrDefault();
            return View(objProduct);
        }
    }
}