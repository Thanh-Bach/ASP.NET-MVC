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
    public class CategoryController : Controller
    {
        WebASPEntities1 objWebASPEntities = new WebASPEntities1();
        // GET: Admin/Category
        public ActionResult Index(string SearchString, string currentFilter, int? page)
        {
            var lstCategory = new List<Category_2119110193>();
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
                lstCategory = objWebASPEntities.Category_2119110193.Where(n => n.Name.Contains(SearchString)).ToList();
            }
            else
            {
                lstCategory = objWebASPEntities.Category_2119110193.ToList();
            }
            ViewBag.currentFilter = SearchString;
            //số lượng items của 1 trang = 4
            int pageSize = 4;
            int pageNumber = (page ?? 1);
            //sắp xếp theo id sản phẩm
            lstCategory = lstCategory.OrderByDescending(n => n.Id).ToList();
            return View(lstCategory.ToPagedList(pageNumber, pageSize));
        }
        [HttpGet]
        public ActionResult Create()
        {
          
            return View();
        }
        [HttpPost]
        public ActionResult Create(Category_2119110193 objCategory)
        {
            
            if (ModelState.IsValid)
            {
                try
                {
                    if (objCategory.ImageUpLoad != null)
                    {
                        string fileName = Path.GetFileNameWithoutExtension(objCategory.ImageUpLoad.FileName);
                        string extension = Path.GetExtension(objCategory.ImageUpLoad.FileName);
                        fileName = fileName + "_" + long.Parse(DateTime.Now.ToString("yyyyMMddhhmmss")) + extension;
                        objCategory.Avatar = fileName;
                        objCategory.ImageUpLoad.SaveAs(Path.Combine(Server.MapPath("~/Content/images/items/"), fileName));
                    }
                    objCategory.CreatedOnUtc = DateTime.Now;
                    objWebASPEntities.Category_2119110193.Add(objCategory);
                    objWebASPEntities.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch
                {
                    return View();
                }
            }
            return View(objCategory);
        }
        [HttpGet]
        public ActionResult Details(int id)
        {
            var objCategory = objWebASPEntities.Category_2119110193.Where(n => n.Id == id).FirstOrDefault();
            return View(objCategory);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var objCategory = objWebASPEntities.Category_2119110193.Where(n => n.Id == id).FirstOrDefault();
            return View(objCategory);
        }

        [HttpPost]
        public ActionResult Delete(Category_2119110193 objcat)
        {
            var objCategory = objWebASPEntities.Category_2119110193.Where(n => n.Id == objcat.Id).FirstOrDefault();
            objWebASPEntities.Category_2119110193.Remove(objCategory);
            objWebASPEntities.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var objCategory = objWebASPEntities.Category_2119110193.Where(n => n.Id == id).FirstOrDefault();
            return View(objCategory);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(Category_2119110193 objCategory)
        {
            if (objCategory.ImageUpLoad != null)
            {
                string fileName = Path.GetFileNameWithoutExtension(objCategory.ImageUpLoad.FileName);
                string extension = Path.GetExtension(objCategory.ImageUpLoad.FileName);
                fileName += extension;
                objCategory.Avatar = fileName;
                objCategory.ImageUpLoad.SaveAs(Path.Combine(Server.MapPath("~/Content/images/items/"), fileName));
            }
            objWebASPEntities.Entry(objCategory).State = EntityState.Modified;
            objWebASPEntities.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Trash()
        {
            var lstCat = new List<Category_2119110193>();
            lstCat = objWebASPEntities.Category_2119110193.Where(m => m.Deleted == true).ToList();
            return View(lstCat);
        }
        public ActionResult DelTrash(int Id)
        {
            Category_2119110193 cate = objWebASPEntities.Category_2119110193.Find(Id);

            cate.Deleted = true;
            objWebASPEntities.SaveChanges();
            return RedirectToAction("Trash", "Category");
        }
        public ActionResult ReTrash(int Id)
        {
            Category_2119110193 cate = objWebASPEntities.Category_2119110193.Find(Id);
            cate.Deleted = false;
            cate.ShowOnHomePage = false;
            objWebASPEntities.SaveChanges();
            return RedirectToAction("Trash", "Category");
        }
        public ActionResult ShowOnHomePage(int Id, bool flag)
        {
            Category_2119110193 cate = objWebASPEntities.Category_2119110193.Find(Id);
            cate.ShowOnHomePage = flag;
            objWebASPEntities.SaveChanges();
            return RedirectToAction("Index", "Category");
        }
    }
}