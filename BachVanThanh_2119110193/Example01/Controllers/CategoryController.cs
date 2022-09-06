using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Example01.Context;

namespace Example01.Controllers
{
    public class CategotyController : Controller
    {
        WebASPEntities1 objWebASPEntities = new WebASPEntities1();
        // GET: Login
        public ActionResult Index()
        {
            var lstCategory = objWebASPEntities.Category_2119110193.ToList(); 
            return View(lstCategory);
        }

        public ActionResult ProductCategory(int Id)
        {
            var lstprd = objWebASPEntities.Product_2119110193.Where(n => n.CategoryId == Id).ToList(); 
            return View(lstprd);
        }
    }
}