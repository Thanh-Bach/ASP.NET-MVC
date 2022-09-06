using Example01.Context;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static Example01.Common;
using Example01.Models;

namespace Example01.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        WebASPEntities1 objWebASPEntities = new WebASPEntities1();
        // GET: Admin/Product
        public ActionResult Index(string SearchString, string currentFilter, int? page)
        {
            //var lstProduct = objWebASPEntities.Product_2119110193.ToList();
            //var lstProduct = objWebASPEntities.Product_2119110193.Where(n => n.Name.Contains(SearchString)).ToList();
            var lstProduct = new List<Product_2119110193>();
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
                lstProduct = objWebASPEntities.Product_2119110193.Where(n => n.Name.Contains(SearchString)).ToList();
            }
            else
            {
                lstProduct = objWebASPEntities.Product_2119110193.ToList();
            }
            ViewBag.currentFilter = SearchString;
            //số lượng items của 1 trang = 4
            int pageSize = 4;
            int pageNumber = (page ?? 1);
            //sắp xếp theo id sản phẩm
            lstProduct = lstProduct.OrderByDescending(n => n.Id).ToList();
            return View(lstProduct.ToPagedList(pageNumber, pageSize));
        }

        [HttpGet]
        public ActionResult Create()
        {
            this.LoadData();
            return View();
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Create(Product_2119110193 objProduct)
        {
            this.LoadData();
            if (ModelState.IsValid)
            {
                try
                {
                    if (objProduct.ImageUpLoad != null)
                    {
                        string fileName = Path.GetFileNameWithoutExtension(objProduct.ImageUpLoad.FileName);
                        string extension = Path.GetExtension(objProduct.ImageUpLoad.FileName);
                        fileName = fileName + "_" + long.Parse(DateTime.Now.ToString("yyyyMMddhhmmss")) + extension;
                        objProduct.Avatar = fileName;
                        objProduct.ImageUpLoad.SaveAs(Path.Combine(Server.MapPath("~/Content/images/items/"), fileName));
                    }
                    objProduct.CreatedOnUtc = DateTime.Now;
                    objWebASPEntities.Product_2119110193.Add(objProduct);
                    objWebASPEntities.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch
                {
                    return View();
                }
            }
            return View(objProduct);
        }
        void LoadData()
        {
            Common objCommon = new Common();
            //lấy danh sách danh mục dưới csdl
            var lstcat = objWebASPEntities.Category_2119110193.ToList();
            //convert sang select list dang value, list
            ListtoDataTableConverter converter = new ListtoDataTableConverter();
            DataTable dtCategory = converter.ToDataTable(lstcat);
            ViewBag.ListCategory = objCommon.ToSelectList(dtCategory, "Id", "Name");

            //lấy danh sách thương hiệu dưới csdl
            var lstBrand = objWebASPEntities.Brand_2119110193.ToList();
            //convert sang select list dang value, list
            DataTable dtBrand = converter.ToDataTable(lstBrand);
            ViewBag.ListBrand = objCommon.ToSelectList(dtBrand, "Id", "Name");

            //
            List<ProductType> lstProductType = new List<ProductType>();
            ProductType objProductType = new ProductType();
            objProductType.Id = 01;
            objProductType.Name = "Giảm giá sốc";
            lstProductType.Add(objProductType);

            objProductType = new ProductType();
            objProductType.Id = 02;
            objProductType.Name = "Đề xuất";
            lstProductType.Add(objProductType);
            DataTable dataProductType = converter.ToDataTable(lstProductType);
            ViewBag.ListProductType = objCommon.ToSelectList(dataProductType, "Id", "Name");
        }
        [HttpGet]
        public ActionResult Details(int id)
        {
            var objProduct = objWebASPEntities.Product_2119110193.Where(n => n.Id == id).FirstOrDefault();
            return View(objProduct);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var objProduct = objWebASPEntities.Product_2119110193.Where(n => n.Id == id).FirstOrDefault();
            return View(objProduct);
        }

        [HttpPost]
        public ActionResult Delete(Product_2119110193 objPrd)
        {
            var objProduct = objWebASPEntities.Product_2119110193.Where(n => n.Id == objPrd.Id).FirstOrDefault();
            objWebASPEntities.Product_2119110193.Remove(objProduct);
            objWebASPEntities.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var objProduct = objWebASPEntities.Product_2119110193.Where(n => n.Id == id).FirstOrDefault();
            return View(objProduct);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(Product_2119110193 objProduct)
        {
            if(objProduct.ImageUpLoad != null)
            {
                string fileName = Path.GetFileNameWithoutExtension(objProduct.ImageUpLoad.FileName);
                string extension = Path.GetExtension(objProduct.ImageUpLoad.FileName);
                fileName = fileName + "_" + long.Parse(DateTime.Now.ToString("yyyyMMddhhmmss")) + extension;
                objProduct.Avatar = fileName;
                objProduct.ImageUpLoad.SaveAs(Path.Combine(Server.MapPath("~/Content/images/items/"), fileName));
            }
            objWebASPEntities.Entry(objProduct).State = EntityState.Modified;
            objWebASPEntities.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Trash()
        {
            var lstPrd = new List<Product_2119110193>();
            lstPrd = objWebASPEntities.Product_2119110193.Where(m => m.Deleted == true).ToList();
            return View(lstPrd);
        }
        public ActionResult DelTrash(int Id)
        {
            Product_2119110193 Prd = objWebASPEntities.Product_2119110193.Find(Id);

            Prd.Deleted = true;
           
            objWebASPEntities.SaveChanges();
            return RedirectToAction("Trash", "Product");
        }
        public ActionResult ReTrash(int Id)
        {
            Product_2119110193 Prd = objWebASPEntities.Product_2119110193.Find(Id);
            Prd.Deleted = false;
            Prd.ShowOnHomePage = false;
            objWebASPEntities.SaveChanges();
            return RedirectToAction("Trash", "Product");
        }
        public ActionResult ShowOnHomePage(int Id, bool flag)
        {
            Product_2119110193 Prd = objWebASPEntities.Product_2119110193.Find(Id);
            Prd.ShowOnHomePage = flag;
            objWebASPEntities.SaveChanges();
            return RedirectToAction("Index", "Product");
        }
    }
}