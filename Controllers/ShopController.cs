using MVC_Food_Delivery.Models;
using MVC_Food_Delivery.Services;
using MVC_Food_Delivery.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_Food_Delivery.Controllers
{
    public class ShopController : Controller
    {
        private Food_Delivery_DBEntities db = new Food_Delivery_DBEntities();
        private Product_Master_Services Product_Master_Services = new Product_Master_Services();
        // GET: Shop

        public ActionResult AddToCartShoProduct()
        {
            try
            {

                int q = Convert.ToInt32(Session["Userid"]);

                //var p = db.Add_To_Cart_Master.Where(x => x.CustomerID == q).ToList();


                List<Product_Master> product_Masters = db.Product_Master.ToList();
                List<Add_To_Cart_Master> add_To_Cart_Masters = db.Add_To_Cart_Master.ToList();


                var employeeRecord = from e in product_Masters
                                     join d in add_To_Cart_Masters on e.ProductID equals d.ProductID
                                     where d.CustomerID == q

                                     select new CartViewModel
                                     {
                                         Product_Master = e,
                                         Add_To_Cart_Master = d,

                                     };


                var h = employeeRecord.Count();
                Session["count"] = h;

                return View(employeeRecord);
            }
            catch (Exception)
            {
                TempData["dedmessage"] = "Something is Wrong";
                return View();

            }
        }


        public ActionResult DeleteAddtoCartProduct(int AddToCartID)
        {
            try
            {

                Add_To_Cart_Master add_To_Cart_Master = db.Add_To_Cart_Master.Find(AddToCartID);
                db.Add_To_Cart_Master.Remove(add_To_Cart_Master);
                db.SaveChanges();
                TempData["dmessage"] = "Delete Product";
                return RedirectToAction("AddToCartShoProduct");

            }
            catch (Exception)
            {
                TempData["ddmsessage"] = "Something is Wrong";
                return View();
            }
        }

        [HttpPost]
        public ActionResult SaveOrder(Order_Master order_Master)
        {
            try
            {


                int userid = Convert.ToInt32(Session["Userid"]);
                //DateTime dateTime = DateTime.Now;

                //Order_Master model = new Order_Master();

                //model.CustomerID = userid;
                //model.OrderDate = dateTime;
                //model.TotalPrice = order_Master.TotalPrice;

                Session["price"] = order_Master.TotalPrice;
                Session["CustomerID"] = order_Master.CustomerID;
                Session["OrderDate"] = order_Master.OrderDate;
               

                //db.Order_Master.Add(model);
                //db.SaveChanges();

                //return RedirectToAction("Index","Payment");
                return Json(new { status = "OK" });
            }
            catch (Exception)
            {

                TempData["sessage"] = "Something is Wrong";
                return View();
            }
        }


    }
}