using Example01.Context;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Example01.Areas.Admin.Controllers
{
    public class UserController : Controller
    {
        WebASPEntities1 objWebASPEntities = new WebASPEntities1();
        // GET: Admin/User
        public ActionResult Index(string SearchString, string currentFilter, int? page)
        {
            var lstUser = new List<Users_2119110193>();
            if (SearchString != null)
            {
                page = 1;
            }
            else
            {
                SearchString = currentFilter;
            }
            if (!string.IsNullOrEmpty(SearchString))
            {
                lstUser = objWebASPEntities.Users_2119110193.Where(n => n.FirstName.Contains(SearchString)).ToList();
            }
            else
            {
                lstUser = objWebASPEntities.Users_2119110193.ToList();
            }
            ViewBag.currentFilter = SearchString;
            //số lượng items của 1 trang = 4
            int pageSize = 4;
            int pageNumber = (page ?? 1);
            //sắp xếp theo id sản phẩm
            lstUser = lstUser.OrderByDescending(n => n.Id).ToList();
            return View(lstUser.ToPagedList(pageNumber, pageSize));
        }
        [HttpGet]
        public ActionResult Create()
        {

            return View();
        }
        [HttpPost]
        public ActionResult Create(Users_2119110193 objUser)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    var check = objWebASPEntities.Users_2119110193.FirstOrDefault(s => s.Email == objUser.Email);
                    if (check == null)
                    {
                        objUser.Password = GetMD5(objUser.Password);
                        objWebASPEntities.Configuration.ValidateOnSaveEnabled = false;
                    }
                    else
                    {
                        ViewBag.error = "Email đã tồn tại";
                        return View();
                    }
                    objUser.IsAdmin = false;
                    objWebASPEntities.Users_2119110193.Add(objUser);
                    objWebASPEntities.SaveChanges();
                    return RedirectToAction("Index", "User");
                }
                catch (Exception)
                {
                    return View();
                }
            }
            return View(objUser);
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
        [HttpGet]
            public ActionResult Details(int id)
            {
                var objUser = objWebASPEntities.Users_2119110193.Where(n => n.Id == id).FirstOrDefault();
                return View(objUser);
            }

            [HttpGet]
            public ActionResult Delete(int id)
            {
                var objUser = objWebASPEntities.Users_2119110193.Where(n => n.Id == id).FirstOrDefault();
                return View(objUser);
            }

            [HttpPost]
            public ActionResult Delete(Users_2119110193 objUsr)
            {
                var objUser = objWebASPEntities.Users_2119110193.Where(n => n.Id == objUsr.Id).FirstOrDefault();
                objWebASPEntities.Users_2119110193.Remove(objUser);
                objWebASPEntities.SaveChanges();
                return RedirectToAction("Index");
            }

            [HttpGet]
            public ActionResult Edit(int id)
            {
                var objUser = objWebASPEntities.Users_2119110193.Where(n => n.Id == id).FirstOrDefault();
                return View(objUser);
            }

            [HttpPost]
            [ValidateInput(false)]
            public ActionResult Edit(Users_2119110193 objUser)
            {
            objWebASPEntities.Entry(objUser).State = EntityState.Modified;
            objWebASPEntities.SaveChanges();
                return RedirectToAction("Index");
            }
        //public ActionResult Trash()
        //{
        //    var lstUser = new List<Users_2119110193>();
        //    lstUser = objWebASPEntities.Users_2119110193.Where(m => m.Deleted == true).ToList();
        //    return View(lstUser);
        //}
        //public ActionResult DelTrash(int Id)
        //{
        //    Users_2119110193 User = objWebASPEntities.Users_2119110193.Find(Id);

        //    User.Deleted = true;
        //    objWebASPEntities.SaveChanges();
        //    return RedirectToAction("Index", "User");
        //}
        //public ActionResult ReTrash(int Id)
        //{
        //    Users_2119110193 User = objWebASPEntities.Users_2119110193.Find(Id);
        //    User.Deleted = false;
        //    User.ShowOnHomePage = false;
        //    objWebASPEntities.SaveChanges();
        //    return RedirectToAction("Trash", "User");
        //}
        //public ActionResult ShowOnHomePage(int Id, bool flag)
        //{
        //    Users_2119110193 User = objWebASPEntities.Users_2119110193.Find(Id);
        //    User.ShowOnHomePage = flag;
        //    objWebASPEntities.SaveChanges();
        //    return RedirectToAction("Index", "User");
        //}
    }
    }