using Example01.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Example01.Areas.Admin.Controllers
{
    public class AuthController : Controller
    {
        WebASPEntities1 objWebASPEntities = new WebASPEntities1();
        // GET: Admin/Auth
        [HttpGet]
        public ActionResult Login()
        {
           
                return View();
        }
        [HttpPost]
        
        public ActionResult Login(FormCollection field)
        {
            string strerror = "";
            string email = field["email"];
            string password = field["password"];
            
            var f_password = GetMD5(password);
            var data = objWebASPEntities.Users_2119110193.Where(m => m.IsAdmin == true && (m.Email == email)).FirstOrDefault();
            if (data == null)
            {
                strerror = "Tên đăng nhập không tồn tại";
            }
            else { 
                if (data.Password.Equals(f_password))
                {
                    Session["FirstName"] = data.FirstName;
                    Session["LastName"] = data.LastName;
                    Session["Email"] = data.Email;
                    Session["idUser"] = data.Id;
                    return RedirectToAction("Index","Home");
                }
                else
                {
                    strerror = "Mật khẩu không tồn tại";
                }
                
            }
            ViewBag.Error = strerror;
            return View();
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
    }
}