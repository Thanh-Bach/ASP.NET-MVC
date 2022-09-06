using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Example01.Context;

namespace Example01.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        WebASPEntities1 objWebASPEntities = new WebASPEntities1();
        // GET: Admin/Home
        public ActionResult Index()
        {
            return View();
        }
        

    }
}