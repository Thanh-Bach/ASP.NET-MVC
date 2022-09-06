using Example01.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Example01.Context;

namespace Example01.Controllers
{
    public class PaymentController : Controller
    {
        WebASPEntities1 objWebASPEntities = new WebASPEntities1();
        // GET: Payment
        public ActionResult Index()
        {
            if(Session["IdUser"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                //lấy thông tin từ giỏ hàng từ biến session
                var lstCart = (List<CartModel>)Session["cart"];
                //gán dữ liệu cho order
                Order_2119110193 objOrder = new Order_2119110193();
                objOrder.Name = "Donhang-" + DateTime.Now.ToString("yyyyMMddHHmmss");
                objOrder.UserId = int.Parse(Session["idUser"].ToString());
                objOrder.CreatedOnUtc = DateTime.Now;
                
                objOrder.Status = 1;
                objWebASPEntities.Order_2119110193.Add(objOrder);

                objWebASPEntities.SaveChanges();

                //lấy orderid vừa mới tạo lưu vào orderdetail
                int intOrderId = objOrder.Id;
                List<OrderDetail_2119110193> lstOrderDetail = new List<OrderDetail_2119110193>();

                foreach(var item in lstCart)
                {
                    OrderDetail_2119110193 objOrderdetail = new OrderDetail_2119110193();
                    objOrderdetail.Quantity = item.Quantity;
                    objOrderdetail.OrderId = intOrderId;
                    objOrderdetail.Productid = item.Product.Id;
                    lstOrderDetail.Add(objOrderdetail);
                }
                objWebASPEntities.OrderDetail_2119110193.AddRange(lstOrderDetail);
                objWebASPEntities.SaveChanges();
            }
            return View();
        }
    }
}