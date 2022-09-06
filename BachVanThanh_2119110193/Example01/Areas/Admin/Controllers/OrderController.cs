using Example01.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Example01.Areas.Admin.Controllers
{
    public class OrderController : Controller
    {
        WebASPEntities1 objWebASPEntities = new WebASPEntities1();
        // GET: Admin/Order
        public ActionResult Index()
        {
            var lstOrder = objWebASPEntities.Order_2119110193.ToList();
            return View(lstOrder);
        }
        public ActionResult Details(int id)
        {
            var objOrder = objWebASPEntities.Order_2119110193.Where(n => n.Id == id).FirstOrDefault();
            return View(objOrder);
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var objOrder = objWebASPEntities.Order_2119110193.Where(n => n.Id == id).FirstOrDefault();
            return View(objOrder);
        }

        [HttpPost]
        public ActionResult Delete(Order_2119110193 objod)
        {
            var objOrder = objWebASPEntities.Order_2119110193.Where(n => n.Id == objod.Id).FirstOrDefault();
            objWebASPEntities.Order_2119110193.Remove(objOrder);
            objWebASPEntities.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}