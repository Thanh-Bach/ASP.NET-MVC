using Example01.Context;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Example01.Areas.Admin.Controllers
{
    public class BrandController : Controller
    {
        WebASPEntities1 objWebASPEntities = new WebASPEntities1();
        // GET: Admin/Brand
        public ActionResult Index(string SearchString, string currentFilter, int? page)
        {
            
            var lstBrand = new List<Brand_2119110193>();
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
                lstBrand = objWebASPEntities.Brand_2119110193.Where(n => n.Name.Contains(SearchString)).ToList();
            }
            else
            {
                lstBrand = objWebASPEntities.Brand_2119110193.ToList();
            }
            ViewBag.currentFilter = SearchString;
            //số lượng items của 1 trang = 4
            int pageSize = 4;
            int pageNumber = (page ?? 1);
            //sắp xếp theo id sản phẩm
            lstBrand = lstBrand.OrderByDescending(n => n.Id).ToList();
            return View(lstBrand.ToPagedList(pageNumber, pageSize));
        }
        [HttpGet]
        public ActionResult Create()
        {

            return View();
        }
        [HttpPost]
        public ActionResult Create(Brand_2119110193 objBrand)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    if (objBrand.ImageUpLoad != null)
                    {
                        string fileName = Path.GetFileNameWithoutExtension(objBrand.ImageUpLoad.FileName);
                        string extension = Path.GetExtension(objBrand.ImageUpLoad.FileName);
                        fileName = fileName + "_" + long.Parse(DateTime.Now.ToString("yyyyMMddhhmmss")) + extension;
                        objBrand.Avatar = fileName;
                        objBrand.ImageUpLoad.SaveAs(Path.Combine(Server.MapPath("~/Content/images/items/"), fileName));
                    }
                    objBrand.CreatedOnUtc = DateTime.Now;
                    objWebASPEntities.Brand_2119110193.Add(objBrand);
                    objWebASPEntities.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch
                {
                    return View();
                }
            }
            return View(objBrand);
        }
        [HttpGet]
        public ActionResult Details(int id)
        {
            var objBrand = objWebASPEntities.Brand_2119110193.Where(n => n.Id == id).FirstOrDefault();
            return View(objBrand);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var objBrand = objWebASPEntities.Brand_2119110193.Where(n => n.Id == id).FirstOrDefault();
            return View(objBrand);
        }

        [HttpPost]
        public ActionResult Delete(Brand_2119110193 objBrd)
        {
            var objBrand = objWebASPEntities.Brand_2119110193.Where(n => n.Id == objBrd.Id).FirstOrDefault();
            objWebASPEntities.Brand_2119110193.Remove(objBrand);
            objWebASPEntities.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var objBrand = objWebASPEntities.Brand_2119110193.Where(n => n.Id == id).FirstOrDefault();
            return View(objBrand);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(Brand_2119110193 objBrand)
        {
            if (objBrand.ImageUpLoad != null)
            {
                string fileName = Path.GetFileNameWithoutExtension(objBrand.ImageUpLoad.FileName);
                string extension = Path.GetExtension(objBrand.ImageUpLoad.FileName);
                fileName += extension;
                objBrand.Avatar = fileName;
                objBrand.ImageUpLoad.SaveAs(Path.Combine(Server.MapPath("~/Content/images/category/"), fileName));
            }
            objWebASPEntities.Entry(objBrand).State = EntityState.Modified;
            objWebASPEntities.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Trash()
        {
            var lstBrand = new List<Brand_2119110193>();
            lstBrand = objWebASPEntities.Brand_2119110193.Where(m => m.Deleted == true).ToList();
            return View(lstBrand);
        }
        public ActionResult DelTrash(int Id)
        {
            Brand_2119110193 Brand = objWebASPEntities.Brand_2119110193.Find(Id);
            
            Brand.Deleted = true;
            objWebASPEntities.SaveChanges();
            return RedirectToAction("Trash", "Brand");
        }
        public ActionResult ReTrash(int Id)
        {
            Brand_2119110193 Brand = objWebASPEntities.Brand_2119110193.Find(Id);
            Brand.Deleted = false;
            Brand.ShowOnHomePage = false;
            objWebASPEntities.SaveChanges();
            return RedirectToAction("Trash", "Brand");
        }
        public ActionResult ShowOnHomePage(int Id, bool flag)
        {
            Brand_2119110193 Brand = objWebASPEntities.Brand_2119110193.Find(Id);
            Brand.ShowOnHomePage = flag;
            objWebASPEntities.SaveChanges();
            return RedirectToAction("Index", "Brand");
        }
    }

}