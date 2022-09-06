using Example01.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Example01.Models;
using System.Security.Cryptography;
using System.Text;

namespace Example01.Controllers
{
    public class HomeController : Controller
    {
        WebASPEntities1 objWebASPEntities = new WebASPEntities1();
        public ActionResult Index()
        {
            HomeModel objHomeModel = new HomeModel();
            objHomeModel.ListCategory = objWebASPEntities.Category_2119110193.ToList();
            objHomeModel.ListProduct = objWebASPEntities.Product_2119110193.ToList();
            return View(objHomeModel);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(Users_2119110193 _user)
        {
            if (ModelState.IsValid)
            {
                var check = objWebASPEntities.Users_2119110193.FirstOrDefault(s => s.Email == _user.Email);
                if(check == null)
                {
                    _user.Password = GetMD5(_user.Password);
                    objWebASPEntities.Configuration.ValidateOnSaveEnabled = false;
                    objWebASPEntities.Users_2119110193.Add(_user);
                    objWebASPEntities.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.error = "Email already exitsts";
                    return View();
                }
            }
            return View();
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string email, string password)
        {
            if (ModelState.IsValid)
            {


                var f_password = GetMD5(password);
                var data = objWebASPEntities.Users_2119110193.Where(s => s.Email.Equals(email) && s.Password.Equals(f_password)).ToList();
                if (data.Count() > 0)
                {
                    //add session
                    Session["FullName"] = data.FirstOrDefault().FirstName + " " + data.FirstOrDefault().LastName;
                    Session["Email"] = data.FirstOrDefault().Email;
                    Session["idUser"] = data.FirstOrDefault().Id;
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.error = "Login failed";
                    return RedirectToAction("Login");
                }
            }
            return View();
        }
        public ActionResult Logout()
        {
            Session.Clear();//remove session
            return RedirectToAction("Login");
        }
        public static string GetMD5(string str)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] fromData = Encoding.UTF8.GetBytes(str);
            byte[] targetData = md5.ComputeHash(fromData);
            string byte2String = null;
            for (int i = 0; i < targetData.Length; i++)
            {
                byte2String += targetData[i].ToString("x2");
            }
            return byte2String;
        }

        public ActionResult Search(string SearchString)
        {
            var lstProduct = objWebASPEntities.Product_2119110193.Where(n => n.Name.Contains(SearchString)).ToList();
            return View(lstProduct);
        }

        
    }
}